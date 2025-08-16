using Kanban.API.Data;
using Kanban.API.Infrastructure.Persistence.Queries;
using Kanban.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Kanban.API.Infrastructure.Persistence.Repositories;

public class BoardRepository(KanbanDbContext db) : IBoardRepository
{
    public Task<Board?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return BoardQueries.GetById(db, id, ct);
    }

    public Task<List<Board>> GetAllForUserAsync(string userId, CancellationToken ct = default)
    {
        return db.Boards
            .Where(b => b.CreatedByUserId == userId)
            .ToListAsync(ct);
    }

    public Task AddAsync(Board board, CancellationToken ct = default)
    {
        db.Boards.Add(board);
        
        return Task.CompletedTask;
    }
}