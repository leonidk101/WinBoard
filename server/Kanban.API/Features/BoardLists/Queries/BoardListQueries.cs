using Kanban.API.Data;
using Kanban.API.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.EF;

namespace Kanban.API.Features.BoardLists.Queries;

public static class BoardListQueries
{
    public static readonly Func<KanbanDbContext, Guid, CancellationToken, Task<BoardList?>> GetById =
        CompileAsyncQuery<KanbanDbContext, Guid, BoardList?>((db, id, ct) =>
            db.BoardLists.AsNoTracking().FirstOrDefault(bl => bl.Id == id)
        );

    public static readonly Func<KanbanDbContext, Guid, IAsyncEnumerable<BoardList>> GetAllForBoard =
        CompileAsyncQuery((KanbanDbContext db, Guid boardId) => 
            db.BoardLists.AsNoTracking().Where(bl => bl.BoardId == boardId)
        );
}