using ToDo.Domain.Entities;

namespace ToDo.Application.Repositories;

public interface IUserRepository
{
    Task CreateAsync(User? user, CancellationToken cancellationToken);
    Task<User?> TryGetByDataAsync(string email, string password, CancellationToken cancellationToken);
    Task<User?> GetUserById(Guid id, CancellationToken cancellationToken);
}