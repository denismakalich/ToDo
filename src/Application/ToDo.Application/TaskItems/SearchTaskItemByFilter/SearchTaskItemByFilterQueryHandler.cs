using MediatR;
using ToDo.Application.Repositories;
using ToDo.Domain.Entities;

namespace ToDo.Application.TaskItems.SearchTaskItemByFilter;

public class SearchTaskItemByFilterQueryHandler : IRequestHandler<SearchTaskItemByFilterQuery, IEnumerable<TaskItem>>
{
    private readonly ITenantFactory _tenantFactory;

    public SearchTaskItemByFilterQueryHandler(ITenantFactory tenantFactory)
    {
        ArgumentNullException.ThrowIfNull(tenantFactory);

        _tenantFactory = tenantFactory;
    }

    public async Task<IEnumerable<TaskItem>> Handle(SearchTaskItemByFilterQuery request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var tenant = _tenantFactory.GetTenant();

        return await tenant.TaskItems.SeatchTaskItemByFilterAsync(request.Filter, cancellationToken);
    }
}