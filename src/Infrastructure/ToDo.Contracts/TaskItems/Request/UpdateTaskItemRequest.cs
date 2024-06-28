using ToDo.Domain.Entities.Enums;

namespace ToDo.Contracts.TaskItems.Request;

public record UpdateTaskItemRequest(Guid Id, string Title, string Description, int Priority, Status Status);