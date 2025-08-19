using Kanban.API.Models;

namespace Kanban.API.Features.Boards.Repositories;

public interface IBoardsRepository
{
    Task<Board?> GetByIdAsync(Guid id, CancellationToken ct = default);

    Task<List<Board>> GetAllForUserAsync(string userId, CancellationToken ct = default);

    Task AddAsync(Board board, CancellationToken ct = default);

    Task UpdateAsync(Board board, CancellationToken ct = default);
}