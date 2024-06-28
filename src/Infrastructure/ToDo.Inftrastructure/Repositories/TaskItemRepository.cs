using Microsoft.EntityFrameworkCore;
using ToDo.Application.Models.TaskItems;
using ToDo.Application.Repositories;
using ToDo.Domain.Entities;
using ToDo.Domain.Entities.Enums;
using ToDo.Inftrastructure.Context;

namespace ToDo.Inftrastructure.Repositories;

public class TaskItemRepository : ITaskItemRepository
{
    private readonly ApplicationDbContext _context;

    public TaskItemRepository(ApplicationDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        _context = context;
    }

    public Task CreateAsync(TaskItem taskItem, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(taskItem);

        _context.TaskItems.Add(taskItem);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(TaskItem taskItem, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(taskItem);

        _context.Update(taskItem);

        return Task.CompletedTask;
    }

    public async Task<TaskItem> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id can't be empty", nameof(id));

        return await _context.TaskItems.SingleOrDefaultAsync(t => t.Id == id, cancellationToken)
               ?? throw new InvalidOperationException("Task item doesn't exist");
    }

    public async Task<IEnumerable<TaskItem>> SeatchTaskItemByFilterAsync(SearchTaskItemFilter filter,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);

        var taskItems = _context.TaskItems.AsQueryable();

        if (filter.Status != Status.None)
        {
            taskItems = taskItems.Where(t => t.Status == filter.Status);
        }

        taskItems = filter.SortBy switch
        {
            TaskItemSortBy.Priority => taskItems.OrderBy(t => t.Priority),
            TaskItemSortBy.CreatedOn => taskItems.OrderBy(t => t.CreatedOn),
            TaskItemSortBy.ModifiedOn => taskItems.OrderBy(t => t.ModifiedOn),
            _ => taskItems
        };

        return await taskItems.Skip(filter.Page * filter.PageSize).Take(filter.PageSize)
            .ToArrayAsync(cancellationToken);
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id can't be empty", nameof(id));

        _context.TaskItems.Where(t => t.Id == id).ExecuteDelete();

        return Task.CompletedTask;
    }
}