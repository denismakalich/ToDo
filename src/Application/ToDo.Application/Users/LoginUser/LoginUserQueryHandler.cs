using MediatR;
using Microsoft.Extensions.Logging;
using ToDo.Application.Repositories;

namespace ToDo.Application.Users.LoginUser;

public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, Guid>
{
    private readonly ITenantFactory _tenantFactory;
    private readonly ILogger<LoginUserQuery> _logger;

    public LoginUserQueryHandler(ITenantFactory tenantFactory, ILogger<LoginUserQuery> logger)
    {
        ArgumentNullException.ThrowIfNull(tenantFactory);
        ArgumentNullException.ThrowIfNull(logger);

        _tenantFactory = tenantFactory;
        _logger = logger;
    }
    
    public async Task<Guid> Handle(LoginUserQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(request);

            var tenant = _tenantFactory.GetTenant();
            var user = await tenant.Users.TryGetByDataAsync(request.Email, request.Password, cancellationToken);

            if (user is null)
                throw new InvalidOperationException("User with this data not found");

            _logger.LogInformation($"Succes loging user by data: {request.Email}");
            
            return user.Id;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Unseccessful loging by data {request.Email}");
            throw;
        }
    }
}