using Kanban.API.Data;
using Kanban.API.Features.TaskItems.Models;
using Kanban.API.Features.Tasks.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.EF;

namespace Kanban.API.Features.TaskItems.Queries;

public static class TaskItemQueries
{
    public static readonly Func<KanbanDbContext, Guid, IAsyncEnumerable<TaskItemListDto>>
        GetByBoardList = CompileAsyncQuery((KanbanDbContext db, Guid boardListId) =>
            db.TaskItems
                .AsNoTracking()
                .Where(t => t.BoardListId == boardListId)
                .OrderBy(t => t.Order)
                .Select(t => new TaskItemListDto(
                    t.Id, t.Title, t.Description, t.Order, t.CreatedAt)));

    public static readonly Func<KanbanDbContext, Guid, IAsyncEnumerable<TaskItemDto>>
        GetById = CompileAsyncQuery((KanbanDbContext db, Guid id) =>
            db.TaskItems
                .AsNoTracking()
                .Where(t => t.Id == id)
                .Select(t => new TaskItemDto(
                    t.Id, t.BoardListId, t.Title, t.Description, t.Order, 
                    t.CreatedAt, t.CreatedByUserId, t.Version)));
}