using ToDo.Domain.Entities.Enums;

namespace ToDo.Application.Models.TaskItems;

public record SearchTaskItemFilter(int Page, int PageSize, Status Status, TaskItemSortBy SortBy);