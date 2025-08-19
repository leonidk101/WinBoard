using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Kanban.API.Features.Boards.Models;
using Kanban.API.Features.Boards.Repositories;
using Kanban.API.Models;

namespace Kanban.API.Features.Boards.Endpoints;

internal static partial class BoardEndpoints
{
    public static async Task<Results<Created<Guid>, NotFound<string>>>
        CreateBoardForUser(
            CreateBoardDto req, 
            IBoardsRepository repository,
            ClaimsPrincipal user, 
            CancellationToken ct)
    {
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return TypedResults.NotFound("User not found");
        }
            
        var board = new Board
        {
            Id = Guid.NewGuid(),
            Name = req.Name,
            Description = req.Description,
            CreatedByUserId = userId,
        };
            
        await repository.AddAsync(board, ct);
            
        return TypedResults.Created($"/boards/{board.Id}", board.Id);
    }
}