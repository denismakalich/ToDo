using Microsoft.EntityFrameworkCore;
using ToDo.Application.Repositories;
using ToDo.Domain.Entities;
using ToDo.Inftrastructure.Context;

namespace ToDo.Inftrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        
        _context = context;
    }
        
    public Task CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(user);

        _context.Users.Add(user);

        return Task.CompletedTask;
    }

    public async Task<User?> TryGetByDataAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email can't null/empty/whiteSpace");
        
        if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("password can't null/empty/whiteSpace");

        var user = await _context.Users.FirstOrDefaultAsync(a => a.Email == email);
        
        if (user == null)
            return null;
        
        if (!user.VerifyByPassword(password))
            return null;
        
        return await _context.Users.SingleOrDefaultAsync(u => u.Id == user.Id);
    }
}