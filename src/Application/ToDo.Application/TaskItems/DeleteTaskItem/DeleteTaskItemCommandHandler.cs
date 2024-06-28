using MediatR;
using ToDo.Application.Repositories;

namespace ToDo.Application.TaskItems.DeleteTaskItem;

public class DeleteTaskItemCommandHandler : IRequestHandler<DeleteTaskItemCommand>
{
    private readonly ITenantFactory _tenantFactory;

    public DeleteTaskItemCommandHandler(ITenantFactory tenantFactory)
    {
        ArgumentNullException.ThrowIfNull(tenantFactory);

        _tenantFactory = tenantFactory;
    }

    public async Task Handle(DeleteTaskItemCommand request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var tenant = _tenantFactory.GetTenant();

        await tenant.TaskItems.DeleteAsync(request.Id, cancellationToken);
        await tenant.CommitAsync(cancellationToken);
    }
}