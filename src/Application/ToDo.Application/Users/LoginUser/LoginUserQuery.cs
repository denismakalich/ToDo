using MediatR;

namespace ToDo.Application.Users.LoginUser;

public record LoginUserQuery(string Email, string Password) : IRequest<Guid>;