using Kanban.API.Features.BoardLists.Repositories;
using Kanban.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Kanban.API.Features.BoardLists.Endpoints;

internal static partial class BoardListEndpoints
{
    public static async Task<Results<Ok<BoardList>, NotFound>> 
        GetById(Guid boardId, Guid id, IBoardListRepository repository, CancellationToken ct)
    {
        var boardList = await repository.GetByIdAsync(id, ct);
        
        if (boardList == null || boardList.BoardId != boardId)
        {
            return TypedResults.NotFound();
        }
        
        return TypedResults.Ok(boardList);
    }
}