using Kanban.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kanban.API.Data.Configurations
{
    public class BoardListEntityTypeConfiguration : IEntityTypeConfiguration<BoardList>
    {
        public void Configure(EntityTypeBuilder<BoardList> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Order)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnType("timestamptz")
                .HasDefaultValue("now()");

            builder.Property(x => x.CreatedByUserId)
                .IsRequired()
                .HasMaxLength(450);

            builder.HasOne(x => x.CreatedByUser)
                .WithMany()
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Board>()
                .WithMany()
                .HasForeignKey(x => x.BoardId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.BoardId, x.Order })
                .IsUnique();

            builder.Property(x => x.Version)
                .IsRowVersion();
        }
    }
}
