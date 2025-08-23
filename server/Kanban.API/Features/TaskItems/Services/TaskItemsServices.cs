using Kanban.API.Data;
using Kanban.API.Features.TaskItems.Models;
using Kanban.API.Features.TaskItems.Queries;
using Kanban.API.Features.Tasks.Models;
using Kanban.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Kanban.API.Features.TaskItems.Services;

public class TaskItemsServices(KanbanDbContext db) : ITaskItemsServices
{
    public async Task<TaskItemDto?> GetById(Guid id, CancellationToken ct)
    {
        await foreach (var t in TaskItemQueries.GetById(db, id).WithCancellation(ct))
        {
            return t;
        }
        
        return null;
    }
    
    public async Task<IList<TaskItemListDto>> ListByList(Guid boardListId, CancellationToken ct)
    {
        var result = new List<TaskItemListDto>();
        
        await foreach (var t in TaskItemQueries.GetByBoardList(db, boardListId).WithCancellation(ct))
        {
            result.Add(t);
        }
        
        return result;
    }

    public async Task Create(NewTaskItemDto dto, string userId, CancellationToken ct)
    {
        var maxOrder = await db.TaskItems
            .Where(t => t.BoardListId == dto.BoardListId)
            .Select(t => (int?)t.Order)
            .MaxAsync(ct) ?? -1;
        
        var entity = new TaskItem
        {
            Id = Guid.NewGuid(),
            BoardListId = dto.BoardListId,
            Title = dto.Title.Trim(),
            Description = string.IsNullOrWhiteSpace(dto.Description) ? null : dto.Description.Trim(),
            Order = maxOrder + 1,
            CreatedByUserId = userId
        };

        db.TaskItems.Add(entity);
        
        await db.SaveChangesAsync(ct);
    }
}