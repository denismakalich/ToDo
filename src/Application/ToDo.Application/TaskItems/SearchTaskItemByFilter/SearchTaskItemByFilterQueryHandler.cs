using MediatR;
using Microsoft.Extensions.Logging;
using ToDo.Application.Repositories;
using ToDo.Domain.Entities;

namespace ToDo.Application.TaskItems.SearchTaskItemByFilter;

public class SearchTaskItemByFilterQueryHandler : IRequestHandler<SearchTaskItemByFilterQuery, IEnumerable<TaskItem>>
{
    private readonly ITenantFactory _tenantFactory;
    private readonly ILogger<SearchTaskItemByFilterQuery> _logger;

    public SearchTaskItemByFilterQueryHandler(ITenantFactory tenantFactory, ILogger<SearchTaskItemByFilterQuery> logger)
    {
        ArgumentNullException.ThrowIfNull(tenantFactory);
        ArgumentNullException.ThrowIfNull(logger);

        _tenantFactory = tenantFactory;
        _logger = logger;
    }

    public async Task<IEnumerable<TaskItem>> Handle(SearchTaskItemByFilterQuery request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(request);

            var tenant = _tenantFactory.GetTenant();

            _logger.LogInformation("Successful retrieval by search");
            
            return await tenant.TaskItems.SeatchTaskItemByFilterAsync(request.Filter, cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred during the search.");
            throw;
        }
    }
}