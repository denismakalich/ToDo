using MediatR;
using ToDo.Domain.Entities.Enums;

namespace ToDo.Application.TaskItems.UpdateTaskItem;

public record UpdateTaskItemCommand(Guid Id, string Title, string Description, int Priority, Status Status) : IRequest;