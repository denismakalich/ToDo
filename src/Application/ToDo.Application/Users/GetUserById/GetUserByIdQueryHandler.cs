using MediatR;
using Microsoft.Extensions.Logging;
using ToDo.Application.Repositories;
using ToDo.Domain.Entities;

namespace ToDo.Application.Users.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
{
    private readonly ITenantFactory _tenantFactory;
    private readonly ILogger<GetUserByIdQuery> _logger;

    public GetUserByIdQueryHandler(ITenantFactory tenantFactory, ILogger<GetUserByIdQuery> logger)
    {
        ArgumentNullException.ThrowIfNull(tenantFactory);
        ArgumentNullException.ThrowIfNull(logger);

        _tenantFactory = tenantFactory;
        _logger = logger;
    }

    public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(request);

            var tenant = _tenantFactory.GetTenant();

            _logger.LogInformation($"Succes get user by id: {request.Id}");
            
            return await tenant.Users.GetUserById(request.Id, cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError($"Unsuccesful get user by id: {request.Id}");
            throw;
        }
    }
}