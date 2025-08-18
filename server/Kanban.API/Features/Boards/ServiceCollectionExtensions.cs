using Kanban.API.Features.Boards.Repositories;
using Kanban.API.Features.Boards.Services;

namespace Kanban.API.Features.Boards;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBoardsApi(this IServiceCollection services)
    {
        services.AddScoped<IBoardsRepository, BoardsRepository>();

        services.AddScoped<IBoardsService, BoardsService>();
        
        return services;
    }
}