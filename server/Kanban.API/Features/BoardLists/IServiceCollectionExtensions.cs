using Kanban.API.Features.BoardLists.Repositories;

namespace Kanban.API.Features.BoardLists;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBoardListsApi(this IServiceCollection services)
    {
        services.AddScoped<IBoardListRepository, BoardListRepository>();

        return services;
    }
}