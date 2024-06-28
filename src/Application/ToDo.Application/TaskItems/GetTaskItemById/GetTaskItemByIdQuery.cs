using MediatR;
using ToDo.Domain.Entities;

namespace ToDo.Application.TaskItems.GetTaskItemById;

public record GetTaskItemByIdQuery(Guid Id) : IRequest<TaskItem>;