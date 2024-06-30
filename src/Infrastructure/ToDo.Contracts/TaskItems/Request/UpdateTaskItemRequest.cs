using ToDo.Domain.Entities.Enums;

namespace ToDo.Contracts.TaskItems.Request;

public record UpdateTaskItemRequest(string Title, string Description, int Priority, Status Status);