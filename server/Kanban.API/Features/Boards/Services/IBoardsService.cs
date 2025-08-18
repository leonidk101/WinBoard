using Kanban.API.Models;

namespace Kanban.API.Features.Boards.Services;

public interface IBoardsService
{
    Task<Board?> GetByIdAsync(
        Guid boardId,
        CancellationToken ct = default
    );
}