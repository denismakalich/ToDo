using MediatR;
using ToDo.Application.Repositories;
using ToDo.Domain.Entities;

namespace ToDo.Application.TaskItems.GetTaskItemById;

public class GetTaskItemByIdQueryHandler : IRequestHandler<GetTaskItemByIdQuery, TaskItem>
{
    private readonly ITenantFactory _tenantFactory;

    public GetTaskItemByIdQueryHandler(ITenantFactory tenantFactory)
    {
        _tenantFactory = tenantFactory;
    }
    
    public async Task<TaskItem> Handle(GetTaskItemByIdQuery request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var tenant = _tenantFactory.GetTenant();

        return await tenant.TaskItems.GetByIdAsync(request.Id, cancellationToken);
    }
}