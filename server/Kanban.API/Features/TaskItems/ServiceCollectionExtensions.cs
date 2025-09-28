using Kanban.API.Features.TaskItems.Services;

namespace Kanban.API.Features.TaskItems;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTaskItemsApi(this IServiceCollection services)
    {
        services.AddScoped<ITaskItemsService, TaskItemsService>();
        return services;
    }
}