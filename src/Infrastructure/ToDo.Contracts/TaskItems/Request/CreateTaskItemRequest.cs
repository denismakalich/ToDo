using ToDo.Domain.Entities.Enums;

namespace ToDo.Contracts.TaskItems.Request;

public record CreateTaskItemRequest(string Title, string Description, int Priority, Status Status, Guid UserId);