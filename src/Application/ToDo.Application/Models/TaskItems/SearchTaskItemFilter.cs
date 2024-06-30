using ToDo.Domain.Entities.Enums;

namespace ToDo.Application.Models.TaskItems;

public record SearchTaskItemFilter(int Page, int PageSize, Status Status, Guid UserId, TaskItemSortBy SortBy);