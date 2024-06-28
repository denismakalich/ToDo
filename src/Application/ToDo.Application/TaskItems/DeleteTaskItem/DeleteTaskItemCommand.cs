using MediatR;

namespace ToDo.Application.TaskItems.DeleteTaskItem;

public record DeleteTaskItemCommand(Guid Id) : IRequest;