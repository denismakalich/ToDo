using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.Domain.Entities;

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
    }
}