using Kanban.API.Data;
using Kanban.API.Features.Boards.Queries;
using Kanban.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Kanban.API.Features.Boards.Repositories;

public class BoardsRepository(KanbanDbContext db) : IBoardsRepository
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

        return db.SaveChangesAsync(ct);
    }

    public Task UpdateAsync(Board board, CancellationToken ct = default)
    {
        db.Boards.Update(board);

        return db.SaveChangesAsync(ct);
    }
}