using ToDo.Domain.Entities.Enums;

namespace ToDo.Contracts.TaskItems.Response;

public record GetTaskItemByIdResponse(
    Guid Id,
    string Title,
    string Description,
    int Priority,
    Status Status,
    DateTimeOffset CreatedOn,
    DateTimeOffset ModifiedOn,
    Guid UserId);