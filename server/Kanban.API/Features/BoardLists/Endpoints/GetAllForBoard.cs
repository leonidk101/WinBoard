using Kanban.API.Features.BoardLists.Repositories;
using Kanban.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Kanban.API.Features.BoardLists.Endpoints;

internal static partial class BoardListEndpoints
{
    public static async Task<Ok<List<BoardList>>> 
        GetAllForBoard(Guid boardId, IBoardListRepository repository, CancellationToken ct)
    {
        var boardLists = await repository.GetAllForBoardAsync(boardId, ct);
        return TypedResults.Ok(boardLists);
    }
}