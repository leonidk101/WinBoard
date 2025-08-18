using System.Security.Claims;
using Kanban.API.Features.Boards.Repositories;
using Kanban.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Kanban.API.Features.Boards.Endpoints;

internal static partial class BoardEndpoints
{
    private static async Task<Results< Ok<List<Board>>, NotFound<string> >> 
        GetAllForTheCurrentUser(IBoardsRepository repository, ClaimsPrincipal user, CancellationToken ct)
    {
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return TypedResults.NotFound("User not found");
        }
            
        var boards = await repository.GetAllForUserAsync(userId, ct);

        return TypedResults.Ok(boards);
    }
}