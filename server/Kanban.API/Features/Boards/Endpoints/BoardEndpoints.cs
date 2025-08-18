namespace Kanban.API.Features.Boards.Endpoints;

internal static partial class BoardEndpoints
{
    public static IEndpointRouteBuilder MapBoardEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/boards")
            .RequireAuthorization();

        group.MapGet("/", GetAllForTheCurrentUser);

        group.MapGet("/{id:guid}", GetById);

        group.MapPost("/", CreateBoardForUser);
        return app;
    }
}
