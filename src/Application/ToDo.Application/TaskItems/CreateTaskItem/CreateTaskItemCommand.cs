using MediatR;
using ToDo.Domain.Entities.Enums;

namespace ToDo.Application.TaskItems.CreateTaskItem;

public record CreateTaskItemCommand(string Title, string Description, int Priority, Status Status, Guid UserId) : IRequest<Guid>;