using MediatR;
using ToDo.Application.Models.TaskItems;
using ToDo.Domain.Entities;

namespace ToDo.Application.TaskItems.SearchTaskItemByFilter;

public record SearchTaskItemByFilterQuery(SearchTaskItemFilter Filter) : IRequest<IEnumerable<TaskItem>>;