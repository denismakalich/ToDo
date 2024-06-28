using ToDo.Application.Models.TaskItems;
using ToDo.Domain.Entities.Enums;

namespace ToDo.Contracts.TaskItems.Request;

public record SearchTaskItemRequest(int Page, int PageSize, Status Status, TaskItemSortBy SortBy);