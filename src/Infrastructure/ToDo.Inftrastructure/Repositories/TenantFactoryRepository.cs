using ToDo.Application.Repositories;
using ToDo.Inftrastructure.Context;

namespace ToDo.Inftrastructure.Repositories;

public class TenantFactoryRepository : ITenantFactory
{
    private readonly Lazy<ITenant> _tenant;

    public TenantFactoryRepository(ApplicationDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        var tenant = new TenantRepository(context);
        _tenant = new Lazy<ITenant>(tenant);
    }

    public ITenant GetTenant()
    {
        return _tenant.Value;
    }
}