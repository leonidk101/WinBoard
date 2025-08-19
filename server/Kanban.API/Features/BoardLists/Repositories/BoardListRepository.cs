using Kanban.API.Data;
using Kanban.API.Features.BoardLists.Queries;
using Kanban.API.Models;

namespace Kanban.API.Features.BoardLists.Repositories;

public class BoardListRepository(KanbanDbContext db) : IBoardListRepository
{
    public Task<BoardList?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return BoardListQueries.GetById(db, id, ct);
    }

    public async Task<List<BoardList>> GetAllForBoardAsync(Guid boardId, CancellationToken ct = default)
    {
        var results = new List<BoardList>();
        await foreach (var boardList in BoardListQueries.GetAllForBoard(db, boardId).WithCancellation(ct))
        {
            results.Add(boardList);
        }
        return results;
    }

    public Task AddAsync(BoardList boardList, CancellationToken ct = default)
    {
        db.BoardLists.Add(boardList);
        
        return db.SaveChangesAsync(ct);
    }

    public Task UpdateAsync(BoardList boardList, CancellationToken ct = default)
    {
        db.BoardLists.Update(boardList);
        
        return db.SaveChangesAsync(ct);
    }

    public Task DeleteAsync(BoardList boardList, CancellationToken ct = default)
    {
        db.BoardLists.Remove(boardList);
        
        return db.SaveChangesAsync(ct);
    }
}