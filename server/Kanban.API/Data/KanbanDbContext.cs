using Microsoft.EntityFrameworkCore;
using Kanban.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Kanban.API.Common;

namespace Kanban.API.Data;

public class KanbanDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public KanbanDbContext(DbContextOptions<KanbanDbContext> options) : base(options)
    {
    }
    
    public DbSet<Board> Boards { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationRole>().HasData(
            new ApplicationRole
            {
                Id = "1",
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new ApplicationRole
            {
                Id = "2",
                Name = "User",
                NormalizedName = "USER"
            }
        );

        modelBuilder.Entity<Board>(entity =>
        {
            // Primary key
            entity.HasKey(e => e.Id);

            // Name property configuration
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Description property configuration
            entity.Property(e => e.Description)
                .HasMaxLength(500);

            // CreatedById property configuration
            entity.Property(e => e.CreatedById)
                .HasMaxLength(450);

            entity.Property(e => e.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("NOW()");

            entity.Property(e => e.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("NOW()");

            entity.HasOne(e => e.CreatedBy)
                .WithMany()
                .HasForeignKey(e => e.CreatedById)
                .OnDelete(DeleteBehavior.SetNull);

            entity.ToTable("Boards");

            entity.HasIndex(e => e.CreatedById)
                .HasDatabaseName("IX_Boards_CreatedById");

            entity.HasIndex(e => e.CreatedAt)
                .HasDatabaseName("IX_Boards_CreatedAt");
        });

        modelBuilder.Entity<Models.Task>(entity =>
        {
            
        });
    }
}
