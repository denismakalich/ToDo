namespace ToDo.Application.Repositories;

public interface ITenant
{
    ITaskItemRepository TaskItems { get; }
    IUserRepository Users { get; }

    Task CommitAsync(CancellationToken cancellationToken);
}