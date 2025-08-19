namespace Kanban.API.Features.BoardLists.Endpoints;

internal static partial class BoardListEndpoints
{
    public static IEndpointRouteBuilder MapBoardListEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/boards/{boardId:guid}/lists")
            .RequireAuthorization();

        group.MapGet("/", GetAllForBoard);

        group.MapGet("/{id:guid}", GetById);

        group.MapPost("/", CreateBoardList);

        group.MapPut("/{id:guid}", UpdateBoardList);

        group.MapDelete("/{id:guid}", DeleteBoardList);

        return app;
    }
}