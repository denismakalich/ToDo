using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

namespace ToDo.Domain.Entities;

public class User
{
    public Guid Id { get; private init; }
    public string? Email { get; private set; }
    public string? NormalizedEmail { get; private set; }
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

    public static User? Create(string email, string password)
    {
        Guid id = Guid.NewGuid();

        return new User(id, email, password);
    }

    [MemberNotNullWhen(true, nameof(Salt))]
    [MemberNotNullWhen(true, nameof(PasswordHash))]
    public bool IsEmailAuthorization()
    {
        return NormalizedEmail is not null && Salt is not null && PasswordHash is not null;
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
        NormalizedEmail = email.ToUpperInvariant();
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

    [MemberNotNull(nameof(NormalizedEmail))]
    [MemberNotNull(nameof(Salt))]
    [MemberNotNull(nameof(PasswordHash))]
    private void ThrowIfNotEmailAuthorization()
    {
        if (!IsEmailAuthorization())
        {
            throw new InvalidOperationException("Not supported email authorization.");
        }
    }

    public bool VerifyByPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(password));
        }

        byte[] saltBytes = Convert.FromBase64String(Salt);
        byte[] passwordHashBytes = Convert.FromBase64String(PasswordHash);

        ThrowIfNotEmailAuthorization();

        using (var hmac = new HMACSHA512(saltBytes))
        {
            var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            var a = hmac.ComputeHash(passwordHash);

            var passwordHashArray = passwordHashBytes;
            var b = hmac.ComputeHash(passwordHashArray);
            return Xor(a, b) && Xor(passwordHash, passwordHashArray);
        }
    }

    private static bool Xor(byte[] a, byte[] b)
    {
        var x = a.Length ^ b.Length;

        for (var i = 0; i < a.Length && i < b.Length; ++i)
        {
            x |= a[i] ^ b[i];
        }

        return x == 0;
    }
}