using Kanban.API.Common;
using Kanban.API.Infrastructure.Persistence.Repositories;
using Kanban.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Kanban.API.Endpoints;

public static class BoardEndpoints
{
    public static IEndpointRouteBuilder MapBoardEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/boards");

        group.MapGet("/{id:guid}", async Task<Results<Ok<Board>, NotFound>>
            (Guid id, IBoardRepository repository, CancellationToken ct) =>
        {
            var board = await repository.GetByIdAsync(id, ct);

            return board == null ? TypedResults.NotFound() : TypedResults.Ok(board);
        });

        group.MapPost("/", async Task<Created<Guid>>
            (CreateBoardRequest req, IBoardRepository repository, IUnitOfWork uow, CancellationToken ct) =>
        {
            var board = new Board
            {
                Id = Guid.NewGuid(),
                Name = req.Name,
                Description = req.Description,
                CreatedByUserId = req.UserId,
            };
            
            await repository.AddAsync(board, ct);
            await uow.SaveChangesAsync(ct);
            
            return TypedResults.Created($"/boards/{board.Id}", board.Id);
        });
        return app;
    }
    
    public sealed record CreateBoardRequest(string Name, string Description, string UserId);
}