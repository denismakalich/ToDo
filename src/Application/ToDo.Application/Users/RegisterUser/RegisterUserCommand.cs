using MediatR;

namespace ToDo.Application.Users.RegisterUser;

public record RegisterUserCommand(string Email, string Password) : IRequest<Guid>;