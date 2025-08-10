using Kanban.API.Common;
using Kanban.API.Infrastructure.Persistence.Repositories;

namespace Kanban.API.Infrastructure.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<IBoardRepository, BoardRepository>();
        
        return services;
    }
}