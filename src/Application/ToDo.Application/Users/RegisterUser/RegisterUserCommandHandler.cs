using MediatR;
using Microsoft.Extensions.Logging;
using ToDo.Application.Repositories;
using ToDo.Domain.Entities;

namespace ToDo.Application.Users.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly ITenantFactory _tenantFactory;
    private readonly ILogger<RegisterUserCommand> _logger;
    
    public RegisterUserCommandHandler(ITenantFactory tenantFactory, ILogger<RegisterUserCommand> logger)
    {
        ArgumentNullException.ThrowIfNull(tenantFactory);
        ArgumentNullException.ThrowIfNull(logger);

        _tenantFactory = tenantFactory;
        _logger = logger;
    }
    
    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(request);

            var tenant = _tenantFactory.GetTenant();

            if (await tenant.Users.TryGetByDataAsync(request.Email, request.Password, cancellationToken) is not null)
                throw new InvalidOperationException("Account already exist");
        
            var user = User.Create(request.Email, request.Password);

            await tenant.Users.CreateAsync(user, cancellationToken);
            await tenant.CommitAsync(cancellationToken);

            _logger.LogInformation("Success register user");
            
            return user.Id;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unsuccess register user");
            throw;
        }
    }
}