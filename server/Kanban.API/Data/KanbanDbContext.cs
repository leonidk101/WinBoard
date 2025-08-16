using Microsoft.EntityFrameworkCore;
using Kanban.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Kanban.API.Data.Configurations;

namespace Kanban.API.Data;

public class KanbanDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public KanbanDbContext(DbContextOptions<KanbanDbContext> options) : base(options)
    {
    }
    
    public DbSet<Board> Boards { get; set; } = null!;

    // Precompiled query to get all boards for a specific user
    public static readonly Func<KanbanDbContext, string, IAsyncEnumerable<Board>> GetBoardsByUserIdQuery =
        EF.CompileAsyncQuery((KanbanDbContext context, string userId) =>
            context.Boards
                .Where(b => b.CreatedByUserId == userId)
                .OrderByDescending(b => b.CreatedAt)
                .AsNoTracking());

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

        new BoardEntityTypeConfiguration().Configure(modelBuilder.Entity<Board>());

        new BoardListEntityTypeConfiguration().Configure(modelBuilder.Entity<BoardList>());

        new TaskEntityTypeConfiguration().Configure(modelBuilder.Entity<TaskItem>());
    }
}
