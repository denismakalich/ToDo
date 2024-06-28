using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

namespace ToDo.Domain.Entities;

public class User
{
    public Guid Id { get; private init; }
    public string? Email { get; private set; }
    public string Salt { get; private set; }
    public string PasswordHash { get; private set; }

    private User()
    {
    }

    public User(Guid id, string email, string password)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id can't be an empty GUID", nameof(id));

        if (string.IsNullOrEmpty(email))
            throw new ArgumentException("Value can't be null or empty.", nameof(email));

        if (string.IsNullOrEmpty(password))
            throw new ArgumentException("Value can't be null or empty.", nameof(password));

        Id = id;
        SetEmail(email);
        SetPassword(password);
    }

    public static User Create(string email, string password)
    {
        Guid id = Guid.NewGuid();

        return new User(id, email, password);
    }
    
    [MemberNotNull(nameof(Salt))]
    [MemberNotNull(nameof(PasswordHash))]
    private void SetPassword(string newPassword)
    {
        byte[] salt, passwordHash;
        using (var hmac = new HMACSHA512())
        {
            salt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(newPassword));
        }

        Salt = Convert.ToBase64String(salt);
        PasswordHash = Convert.ToBase64String(passwordHash);
    }
    
    private void SetEmail(string email)
    {
        ThrowIfEmailIsNotValid(email);

        Email = email;
    }

    private static void ThrowIfEmailIsNotValid(string email)
    {
        if (!email.Contains('@', StringComparison.InvariantCultureIgnoreCase)
            || email[^1] == '@'
            || email[0] == '@')
        {
            throw new InvalidOperationException("Invalid email.");
        }
    }
}