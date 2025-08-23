using Kanban.API.Features.TaskItems.Models;
using Kanban.API.Features.Tasks.Models;

namespace Kanban.API.Features.TaskItems.Services;

public interface ITaskItemsServices
{
    Task<TaskItemDto?> GetById(Guid id, CancellationToken ct);
    
    Task<IList<TaskItemListDto>> ListByList(Guid boardListId, CancellationToken ct);
}