using MediatR;
using Microsoft.Extensions.Logging;
using ToDo.Application.Repositories;
using ToDo.Domain.Entities;

namespace ToDo.Application.TaskItems.GetTaskItemById;

public class GetTaskItemByIdQueryHandler : IRequestHandler<GetTaskItemByIdQuery, TaskItem>
{
    private readonly ITenantFactory _tenantFactory;
    private readonly ILogger<GetTaskItemByIdQuery> _logger;

    public GetTaskItemByIdQueryHandler(ITenantFactory tenantFactory, ILogger<GetTaskItemByIdQuery> logger)
    {
        ArgumentNullException.ThrowIfNull(tenantFactory);
        ArgumentNullException.ThrowIfNull(logger);
        
        _tenantFactory = tenantFactory;
        _logger = logger;
    }
    
    public async Task<TaskItem> Handle(GetTaskItemByIdQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(request);

            var tenant = _tenantFactory.GetTenant();

            _logger.LogInformation($"Get task item by id: {request.Id}");
            
            return await tenant.TaskItems.GetByIdAsync(request.Id, cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving the ID");
            throw;
        }
    }
}