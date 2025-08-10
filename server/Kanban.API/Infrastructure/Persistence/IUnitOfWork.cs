using Microsoft.EntityFrameworkCore.Storage;

namespace Kanban.API.Common;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken ct = default);
    
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken ct = default);
}