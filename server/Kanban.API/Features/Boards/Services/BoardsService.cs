using Kanban.API.Features.Boards.Repositories;
using Kanban.API.Models;

namespace Kanban.API.Features.Boards.Services;

public class BoardsService(IBoardsRepository boardsRepository)
    : IBoardsService
{
    public async Task<Board?> GetByIdAsync(
        Guid boardId, 
        CancellationToken ct = default)
    {
        var board = await boardsRepository.GetByIdAsync(boardId, ct);

        return board;
    }
}