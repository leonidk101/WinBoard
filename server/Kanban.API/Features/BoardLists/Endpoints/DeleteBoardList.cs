using Kanban.API.Features.BoardLists.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Kanban.API.Features.BoardLists.Endpoints;

internal static partial class BoardListEndpoints
{
    public static async Task<Results<NoContent, NotFound>>
        DeleteBoardList(
            Guid boardId,
            Guid id,
            IBoardListRepository repository,
            CancellationToken ct)
    {
        var existingBoardList = await repository.GetByIdAsync(id, ct);
        
        if (existingBoardList == null || existingBoardList.BoardId != boardId)
        {
            return TypedResults.NotFound();
        }

        await repository.DeleteAsync(existingBoardList, ct);
        
        return TypedResults.NoContent();
    }
}