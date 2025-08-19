using System.Security.Claims;
using Kanban.API.Features.BoardLists.Models;
using Kanban.API.Features.BoardLists.Repositories;
using Kanban.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Kanban.API.Features.BoardLists.Endpoints;

internal static partial class BoardListEndpoints
{
    public static async Task<Results<Created<Guid>, NotFound<string>>>
        CreateBoardList(
            Guid boardId,
            CreateBoardListDto req, 
            IBoardListRepository repository,
            ClaimsPrincipal user, 
            CancellationToken ct)
    {
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return TypedResults.NotFound("User not found");
        }
            
        var boardList = new BoardList
        {
            Id = Guid.NewGuid(),
            BoardId = boardId,
            Title = req.Title,
            Order = req.Order,
            CreatedByUserId = userId,
            CreatedAt = DateTimeOffset.UtcNow
        };
            
        await repository.AddAsync(boardList, ct);
            
        return TypedResults.Created($"/boards/{boardId}/lists/{boardList.Id}", boardList.Id);
    }
}