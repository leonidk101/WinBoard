using Kanban.API.Data;
using Kanban.API.Features.TaskItems.Models;
using Kanban.API.Features.TaskItems.Queries;
using Kanban.API.Features.Tasks.Models;
using Kanban.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Kanban.API.Features.TaskItems.Services;

public class TaskItemsService(KanbanDbContext db) : ITaskItemsService
{
    public async Task<TaskItemDto?> GetById(Guid id, CancellationToken ct)
    {
        await foreach (var t in TaskItemQueries.GetById(db, id).WithCancellation(ct))
        {
            return t;
        }
        
        return null;
    }
    
    public async Task<IList<TaskItemListDto>> ListByListAsync(Guid boardListId, CancellationToken ct)
    {
        var result = new List<TaskItemListDto>();
        
        await foreach (var t in TaskItemQueries.GetByBoardList(db, boardListId).WithCancellation(ct))
        {
            result.Add(t);
        }
        
        return result;
    }

    public async Task CreateAsync(NewTaskItemDto dto, string userId, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(dto);

        await using var transaction = await db.Database.BeginTransactionAsync(ct);

        var nextOrder = await db.Database
            .SqlQuery<int>($@"
                UPDATE BoardLists
                    SET LastTaskOrder = LastTaskOrder + 1
                WHERE Id = {dto.BoardListId}
                RETURNING LastTaskOrder")
            .SingleAsync(cancellationToken: ct);
        
        var entity = new TaskItem
        {
            Id = Guid.NewGuid(),
            BoardListId = dto.BoardListId,
            Title = dto.Title.Trim(),
            Description = string.IsNullOrWhiteSpace(dto.Description) ? null : dto.Description.Trim(),
            Order = nextOrder,
            CreatedByUserId = userId
        };

        db.TaskItems.Add(entity);
        
        await db.SaveChangesAsync(ct);
        
        await transaction.CommitAsync(ct);
    }

    public async Task MoveTaskAsync(Guid taskId, Guid destinationBoardId, CancellationToken ct)
    {
        await using var transaction = await db.Database.BeginTransactionAsync(ct);

        var task = await db.TaskItems
            .FirstOrDefaultAsync(t => t.Id == taskId, cancellationToken: ct)
            ?? throw new KeyNotFoundException("Task not found");

        var newOrder = await db.Database
            .SqlQuery<int>($@"
                UPDATE BoardLists
                    SET LastTaskOrder = LastTaskOrder + 1
                WHERE id = {destinationBoardId}
            ")
            .SingleAsync(cancellationToken: ct);
        
        task.BoardListId = destinationBoardId;
        task.Order = newOrder;
        
        await db.SaveChangesAsync(ct);
        await transaction.CommitAsync(ct);
    }
    
    public async Task DeleteTaskAsync(Guid taskId, CancellationToken ct)
    {
        var task = await db.TaskItems.FirstOrDefaultAsync(t => t.Id == taskId, ct)
                   ?? throw new KeyNotFoundException("Task not found");
        
        db.TaskItems.Remove(task);
        await db.SaveChangesAsync(ct);
    }
    
    
}