using MediatR;
using ToDo.Domain.Entities;

namespace ToDo.Application.Users.GetUserById;

public record GetUserByIdQuery(Guid Id) : IRequest<User>;