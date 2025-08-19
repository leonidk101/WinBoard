using Kanban.API.Data;
using Kanban.API.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.EF;

namespace Kanban.API.Features.Boards.Queries;

public static class BoardQueries
{
    public static readonly Func<KanbanDbContext, Guid, CancellationToken, Task<Board?>> GetById =
        CompileAsyncQuery<KanbanDbContext, Guid, Board?>((db, id, ct) =>
            db.Boards.AsNoTracking().FirstOrDefault(e => e.Id == id)
        );

    public static readonly Func<KanbanDbContext, string, IAsyncEnumerable<Board>> GetAllForUser =
        CompileAsyncQuery((KanbanDbContext db, string userId) => 
            db.Boards.AsNoTracking().Where(b => b.CreatedByUserId == userId)
        );
}