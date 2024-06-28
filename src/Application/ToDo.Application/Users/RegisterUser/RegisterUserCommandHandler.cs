using MediatR;
using ToDo.Application.Repositories;
using ToDo.Domain.Entities;

namespace ToDo.Application.Users.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly ITenantFactory _tenantFactory;

    public RegisterUserCommandHandler(ITenantFactory tenantFactory)
    {
        ArgumentNullException.ThrowIfNull(tenantFactory);

        _tenantFactory = tenantFactory;
    }
    
    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var tenant = _tenantFactory.GetTenant();

        if (await tenant.Users.TryGetByDataAsync(request.Email, request.Password, cancellationToken) is not null)
            throw new InvalidOperationException("Account already exist");
        
        var user = User.Create(request.Email, request.Password);

        await tenant.Users.CreateAsync(user, cancellationToken);
        await tenant.CommitAsync(cancellationToken);

        return user.Id;
    }
}