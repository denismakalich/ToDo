using MediatR;
using Microsoft.Extensions.Logging;
using ToDo.Application.Repositories;

namespace ToDo.Application.TaskItems.DeleteTaskItem;

public class DeleteTaskItemCommandHandler : IRequestHandler<DeleteTaskItemCommand>
{
    private readonly ITenantFactory _tenantFactory;
    private readonly ILogger<DeleteTaskItemCommand> _logger;

    public DeleteTaskItemCommandHandler(ITenantFactory tenantFactory, ILogger<DeleteTaskItemCommand> logger)
    {
        ArgumentNullException.ThrowIfNull(tenantFactory);
        ArgumentNullException.ThrowIfNull(logger);

        _tenantFactory = tenantFactory;
        _logger = logger;
    }

    public async Task Handle(DeleteTaskItemCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(request);

            var tenant = _tenantFactory.GetTenant();

            await tenant.TaskItems.DeleteAsync(request.Id, cancellationToken);
            await tenant.CommitAsync(cancellationToken);
            
            _logger.LogInformation("Success delete task item");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred during deletion");
            throw;
        }
    }
}