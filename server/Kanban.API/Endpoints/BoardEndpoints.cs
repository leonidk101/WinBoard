using Kanban.API.Common;
using Kanban.API.Infrastructure.Persistence.Repositories;
using Kanban.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Kanban.API.Endpoints;

public static class BoardEndpoints
{
    public static IEndpointRouteBuilder MapBoardEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/boards")
            .RequireAuthorization();

        group.MapGet("/", async Task<Results<
                Ok<List<Board>>,
                NotFound<string>
            >>
            (IBoardRepository repository, ClaimsPrincipal user, CancellationToken ct) =>
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return TypedResults.NotFound("User not found");
            }
            
            var boards = await repository.GetAllForUserAsync(userId, ct);

            return TypedResults.Ok(boards);
        });

        group.MapGet("/{id:guid}", async Task<Results<Ok<Board>, NotFound>>
            (Guid id, IBoardRepository repository, CancellationToken ct) =>
        {
            var board = await repository.GetByIdAsync(id, ct);

            return board == null ? TypedResults.NotFound() : TypedResults.Ok(board);
        });

        group.MapPost("/", async Task<Results<
                Created<Guid>, 
                NotFound<string>
            >>
            (CreateBoardRequest req, IBoardRepository repository, IUnitOfWork uow, ClaimsPrincipal user, CancellationToken ct) =>
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
            await uow.SaveChangesAsync(ct);
            
            return TypedResults.Created($"/boards/{board.Id}", board.Id);
        });
        return app;
    }
    
    public sealed record CreateBoardRequest(string Name, string Description);
}