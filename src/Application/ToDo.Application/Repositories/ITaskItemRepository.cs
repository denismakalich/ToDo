using ToDo.Application.Models.TaskItems;
using ToDo.Domain.Entities;

namespace ToDo.Application.Repositories;

public interface ITaskItemRepository
{
    Task CreateAsync(TaskItem taskItem, CancellationToken cancellationToken);
    Task UpdateAsync(TaskItem taskItem, CancellationToken cancellationToken);
    Task<TaskItem> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<TaskItem>> SeatchTaskItemByFilterAsync(SearchTaskItemFilter filter,
        CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}