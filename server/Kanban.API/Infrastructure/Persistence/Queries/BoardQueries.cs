using Kanban.API.Data;
using Kanban.API.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.EF;

namespace Kanban.API.Infrastructure.Persistence.Queries;

public static class BoardQueries
{
    public static readonly Func<KanbanDbContext, Guid, CancellationToken, Task<Board?>> GetById =
        CompileAsyncQuery<KanbanDbContext, Guid, Board?>((db, id, ct) =>
            db.Set<Board>().AsNoTracking().FirstOrDefault(e => e.Id == id));
}