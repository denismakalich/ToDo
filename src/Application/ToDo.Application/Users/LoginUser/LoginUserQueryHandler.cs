using MediatR;
using ToDo.Application.Repositories;

namespace ToDo.Application.Users.LoginUser;

public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, Guid>
{
    private readonly ITenantFactory _tenantFactory;

    public LoginUserQueryHandler(ITenantFactory tenantFactory)
    {
        ArgumentNullException.ThrowIfNull(tenantFactory);

        _tenantFactory = tenantFactory;
    }
    
    public async Task<Guid> Handle(LoginUserQuery request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var tenant = _tenantFactory.GetTenant();
        var user = await tenant.Users.TryGetByDataAsync(request.Email, request.Password, cancellationToken);

        if (user is null)
            throw new InvalidOperationException("User with this data not found");

        return user.Id;
    }
}