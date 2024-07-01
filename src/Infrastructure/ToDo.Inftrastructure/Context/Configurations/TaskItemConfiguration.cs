using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.Domain.Entities;
using ToDo.Domain.Entities.Enums;

namespace ToDo.Inftrastructure.Context.Configurations;

public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.ToTable("task_items");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
            .HasColumnName("title")
            .IsRequired();

        builder.Property(t => t.Description)
            .HasColumnName("description")
            .IsRequired();

        builder.Property(t => t.Priority)
            .HasColumnName("priority")
            .IsRequired();

        builder.Property(t => t.Status)
            .HasColumnName("status")
            .IsRequired();

        builder.Property(t => t.CreatedOn)
            .HasColumnName("created_on")
            .IsRequired()
            .HasConversion(
                v => v.UtcDateTime,
                v => new DateTimeOffset(v, TimeSpan.Zero)
            );

        builder.Property(t => t.ModifiedOn)
            .HasColumnName("modified_on")
            .IsRequired()
            .HasConversion(
                v => v.UtcDateTime,
                v => new DateTimeOffset(v, TimeSpan.Zero)
            );

        builder.Property(t => t.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.HasData(new List<TaskItem>
        {
            new(Guid.Parse("E944E01B-E0B5-4DF6-8400-EC64F2C52BC2"), "testing title", "test some descriptions", 10, Status.New, DateTimeOffset.Now, Guid.Parse("1F0CA162-4C04-4C2F-BBDA-86A91F6D1768"))
        });
    }
}