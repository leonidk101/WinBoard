using Kanban.API.Models;

namespace Kanban.API.Features.BoardLists.Repositories;

public interface IBoardListRepository
{
    Task<BoardList?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<BoardList>> GetAllForBoardAsync(Guid boardId, CancellationToken ct = default);
    Task AddAsync(BoardList boardList, CancellationToken ct = default);
    Task UpdateAsync(BoardList boardList, CancellationToken ct = default);
    Task DeleteAsync(BoardList boardList, CancellationToken ct = default);
}