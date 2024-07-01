using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.Domain.Entities;

namespace ToDo.Inftrastructure.Context.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Email)
            .HasColumnName("email")
            .IsRequired();

        builder.Property(u => u.PasswordHash)
            .HasColumnName("passwordHash")
            .IsRequired();

        builder.HasData(new List<User>
        {
            new User(Guid.Parse("1F0CA162-4C04-4C2F-BBDA-86A91F6D1768"), "admin@mail.ru", "adminPassword")
        });
    }
}