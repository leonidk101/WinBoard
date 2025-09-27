using Kanban.API.Features.TaskItems.Models;
using Kanban.API.Features.Tasks.Models;

namespace Kanban.API.Features.TaskItems.Services;

public interface ITaskItemsService
{
    Task<TaskItemDto?> GetById(Guid id, CancellationToken ct);
    
    Task<IList<TaskItemListDto>> ListByListAsync(Guid boardListId, CancellationToken ct);
}