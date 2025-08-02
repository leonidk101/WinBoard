using Microsoft.EntityFrameworkCore;
using Kanban.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Kanban.Web.Data;

public class KanbanDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public KanbanDbContext(DbContextOptions<KanbanDbContext> options) : base(options)
    {
    }
    
    public DbSet<Board> Boards { get; set; } = null!;
    public DbSet<Column> Columns { get; set; } = null!;
    public DbSet<Card> Cards { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Board configuration
        modelBuilder.Entity<Board>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
        });
        
        // Column configuration
        modelBuilder.Entity<Column>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            
            // Foreign key relationship
            entity.HasOne(e => e.Board)
                  .WithMany(b => b.Columns)
                  .HasForeignKey(e => e.BoardId)
                  .OnDelete(DeleteBehavior.Cascade);
                  
            // Index on BoardId for performance
            entity.HasIndex(e => e.BoardId);
        });
        
        // Card configuration
        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Priority).HasConversion<int>();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            
            // Foreign key relationship
            entity.HasOne(e => e.Column)
                  .WithMany(c => c.Cards)
                  .HasForeignKey(e => e.ColumnId)
                  .OnDelete(DeleteBehavior.Cascade);
                  
            // Index on ColumnId for performance
            entity.HasIndex(e => e.ColumnId);
        });
        
        // Seed data
        SeedData(modelBuilder);
    }
    
    private static void SeedData(ModelBuilder modelBuilder)
    {
        // Seed a default board
        modelBuilder.Entity<Board>().HasData(
            new Board
            {
                Id = 1,
                Name = "My Kanban Board",
                Description = "Default Kanban board for project management",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );
        
        // Seed default columns
        modelBuilder.Entity<Column>().HasData(
            new Column
            {
                Id = 1,
                Name = "To Do",
                Position = 1,
                BoardId = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Column
            {
                Id = 2,
                Name = "In Progress",
                Position = 2,
                BoardId = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Column
            {
                Id = 3,
                Name = "Done",
                Position = 3,
                BoardId = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );
        
        // Seed sample cards
        modelBuilder.Entity<Card>().HasData(
            new Card
            {
                Id = 1,
                Title = "Setup Entity Framework",
                Description = "Configure EF Core 10 for the Kanban application",
                Position = 1,
                ColumnId = 2,
                Priority = Priority.High,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Card
            {
                Id = 2,
                Title = "Create API endpoints",
                Description = "Implement REST API for managing boards, columns, and cards",
                Position = 1,
                ColumnId = 1,
                Priority = Priority.Medium,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );
    }
}
