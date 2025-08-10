using Kanban.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kanban.API.Data.Configurations
{
    public class TaskEntityTypeConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.ToTable("Tasks");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .HasMaxLength(4000);

            builder.Property(x => x.CreatedAt)
                .HasColumnType("timestamptz")
                .HasDefaultValue("now()");

            builder.Property(x => x.CreatedByUserId)
                .IsRequired()
                .HasMaxLength(450);

            builder.HasIndex(x => x.BoardListId);

            builder.HasOne<BoardList>()
                .WithMany()
                .HasForeignKey(x => x.BoardListId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new { x.BoardListId, x.Order })
                .IsUnique();

            builder.Property(x => x.Version)
                .IsRowVersion();
        }
    }
}