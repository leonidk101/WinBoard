using Kanban.API.Features.Boards.Repositories;
using Kanban.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Kanban.API.Features.Boards.Endpoints;

internal static partial class BoardEndpoints
{
    private static async Task<Results< Ok<Board>, NotFound>> 
        GetById(Guid id, IBoardsRepository boardsService, CancellationToken ct)
    {
        var board = await boardsService.GetByIdAsync(id, ct);
            
        return board == null ? TypedResults.NotFound() : TypedResults.Ok(board);
    }
}
