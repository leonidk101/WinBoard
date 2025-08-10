using Kanban.API.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Kanban.API.Common;

public class UnitOfWork(KanbanDbContext db) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return db.SaveChangesAsync(ct);
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken ct = default)
    {
        return await db.Database.BeginTransactionAsync(ct);
    }
}