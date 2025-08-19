using Kanban.API.Features.BoardLists.Models;
using Kanban.API.Features.BoardLists.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Kanban.API.Features.BoardLists.Endpoints;

internal static partial class BoardListEndpoints
{
    public static async Task<Results<NoContent, NotFound>>
        UpdateBoardList(
            Guid boardId,
            Guid id, 
            UpdateBoardListDto req,
            IBoardListRepository repository,
            CancellationToken ct)
    {
        var existingBoardList = await repository.GetByIdAsync(id, ct);
        
        if (existingBoardList == null || existingBoardList.BoardId != boardId)
        {
            return TypedResults.NotFound();
        }

        existingBoardList.Title = req.Title;
        existingBoardList.Order = req.Order;
        
        await repository.UpdateAsync(existingBoardList, ct);
        
        return TypedResults.NoContent();
    }
}