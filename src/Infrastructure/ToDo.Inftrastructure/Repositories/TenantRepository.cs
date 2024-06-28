using ToDo.Application.Repositories;
using ToDo.Inftrastructure.Context;

namespace ToDo.Inftrastructure.Repositories;

internal sealed class TenantRepository : ITenant
{
    private readonly ApplicationDbContext _context;
    public ITaskItemRepository TaskItems { get; }
    public IUserRepository Users { get; }

    public TenantRepository(ApplicationDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        _context = context;
        TaskItems = new TaskItemRepository(context);
        Users = new UserRepository(context);
    }

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}