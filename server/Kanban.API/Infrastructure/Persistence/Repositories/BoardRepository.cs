using Kanban.API.Data;
using Kanban.API.Infrastructure.Persistence.Queries;
using Kanban.API.Models;

namespace Kanban.API.Infrastructure.Persistence.Repositories;

public class BoardRepository(KanbanDbContext db) : IBoardRepository
{
    public Task<Board?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return BoardQueries.GetById(db, id, ct);
    }

    public Task AddAsync(Board board, CancellationToken ct = default)
    {
        db.Set<Board>().Add(board);
        
        return Task.CompletedTask;
    }
}