using Kanban.API.Data;
using Kanban.API.Features.TaskItems.Models;
using Kanban.API.Features.TaskItems.Repositories;
using Kanban.API.Features.Tasks.Models;

namespace Kanban.API.Features.Tasks.Repositories;

public class TaskItemsItemsRepository(KanbanDbContext db)
    : ITaskItemsRepository
{
    public async Task CreateTask(NewTaskItemDto newTaskItemDto, CancellationToken ct = default)
    {
        
    }
}