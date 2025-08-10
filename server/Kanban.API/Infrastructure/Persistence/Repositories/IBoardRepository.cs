using Kanban.API.Models;

namespace Kanban.API.Infrastructure.Persistence.Repositories;

public interface IBoardRepository
{
    Task<Board?> GetByIdAsync(Guid id, CancellationToken ct = default);

    Task AddAsync(Board board, CancellationToken ct = default);
}