using MediatR;
using Microsoft.Extensions.Logging;
using ToDo.Application.Repositories;

namespace ToDo.Application.TaskItems.UpdateTaskItem;

public class UpdateTaskItemCommandHandler : IRequestHandler<UpdateTaskItemCommand>
{
    private readonly ITenantFactory _tenantFactory;
    private readonly ILogger<UpdateTaskItemCommand> _logger;

    public UpdateTaskItemCommandHandler(ITenantFactory tenantFactory, ILogger<UpdateTaskItemCommand> logger)
    {
        ArgumentNullException.ThrowIfNull(tenantFactory);
        ArgumentNullException.ThrowIfNull(logger);

        _tenantFactory = tenantFactory;
        _logger = logger;
    }

    public async Task Handle(UpdateTaskItemCommand request, CancellationToken cancellationToken = default)
    {
        try
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
            
            _logger.LogInformation("Success update task item");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occurred during update.");
            throw;
        }
    }
}