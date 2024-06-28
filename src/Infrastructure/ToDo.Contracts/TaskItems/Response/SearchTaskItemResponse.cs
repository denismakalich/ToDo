using ToDo.Domain.Entities;

namespace ToDo.Contracts.TaskItems.Response;

public record SearchTaskItemResponse(IEnumerable<TaskItem> TaskItems);