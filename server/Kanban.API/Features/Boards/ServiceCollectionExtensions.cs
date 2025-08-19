using Kanban.API.Features.Boards.Repositories;

namespace Kanban.API.Features.Boards;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBoardsApi(this IServiceCollection services)
    {
        services.AddScoped<IBoardsRepository, BoardsRepository>();
        
        return services;
    }
}