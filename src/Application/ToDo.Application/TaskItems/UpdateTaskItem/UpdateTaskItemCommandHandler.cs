using MediatR;
using ToDo.Application.Repositories;

namespace ToDo.Application.TaskItems.UpdateTaskItem;

public class UpdateTaskItemCommandHandler : IRequestHandler<UpdateTaskItemCommand>
{
    private readonly ITenantFactory _tenantFactory;

    public UpdateTaskItemCommandHandler(ITenantFactory tenantFactory)
    {
        ArgumentNullException.ThrowIfNull(tenantFactory);

        _tenantFactory = tenantFactory;
    }

    public async Task Handle(UpdateTaskItemCommand request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var tenant = _tenantFactory.GetTenant();
        var taskItem = await tenant.TaskItems.GetByIdAsync(request.Id, cancellationToken);

        taskItem.SetTitle(request.Title);
        taskItem.SetDescription(request.Description);
        taskItem.SetPriority(request.Priority);
        taskItem.SetStatus(request.Status);

        await tenant.TaskItems.UpdateAsync(taskItem, cancellationToken);
        await tenant.CommitAsync(cancellationToken);
    }
}