using MediatR;
using ToDo.Application.Repositories;
using ToDo.Domain.Entities;

namespace ToDo.Application.TaskItems.CreateTaskItem;

public class CreateTaskItemCommandHandler : IRequestHandler<CreateTaskItemCommand, Guid>
{
    private readonly ITenantFactory _tenantFactory;

    public CreateTaskItemCommandHandler(ITenantFactory tenantFactory)
    {
        ArgumentNullException.ThrowIfNull(tenantFactory);

        _tenantFactory = tenantFactory;
    }

    public async Task<Guid> Handle(CreateTaskItemCommand request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var tenant = _tenantFactory.GetTenant();
        var taskItem = TaskItem.Create(request.Title, request.Description, request.Priority, request.Status,
            request.UserId);

        await tenant.TaskItems.CreateAsync(taskItem, cancellationToken);
        await tenant.CommitAsync(cancellationToken);

        return taskItem.Id;
    }
}