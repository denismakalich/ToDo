using MediatR;
using Microsoft.Extensions.Logging;
using ToDo.Application.Repositories;
using ToDo.Domain.Entities;

namespace ToDo.Application.TaskItems.CreateTaskItem;

public class CreateTaskItemCommandHandler : IRequestHandler<CreateTaskItemCommand, Guid>
{
    private readonly ITenantFactory _tenantFactory;
    private readonly ILogger<CreateTaskItemCommand> _logger;

    public CreateTaskItemCommandHandler(ITenantFactory tenantFactory, ILogger<CreateTaskItemCommand> logger)
    {
        ArgumentNullException.ThrowIfNull(tenantFactory);
        ArgumentNullException.ThrowIfNull(logger);

        _tenantFactory = tenantFactory;
        _logger = logger;
    }

    public async Task<Guid> Handle(CreateTaskItemCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(request);

            var tenant = _tenantFactory.GetTenant();
            var taskItem = TaskItem.Create(request.Title, request.Description, request.Priority, request.Status,
                request.UserId);

            await tenant.TaskItems.CreateAsync(taskItem, cancellationToken);
            await tenant.CommitAsync(cancellationToken);

            _logger.LogInformation($"Succes task item created with {taskItem.Id}; {taskItem.Title}");

            return taskItem.Id;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exceptions in CreateTaskItemCommandHandler");
            throw;
        }
    }
}