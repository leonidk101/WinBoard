using Kanban.API.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Kanban.API.Tests;

public class SqliteTestFixture : IAsyncLifetime
{
    private SqliteConnection _connection = null!;
    public DbContextOptions<KanbanDbContext> Options { get; set; } = null!;
    
    public async Task InitializeAsync()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        await _connection.OpenAsync();

        Options = new DbContextOptionsBuilder<KanbanDbContext>()
            .UseSqlite(_connection)
            .EnableSensitiveDataLogging()
            .Options;

        await using var ctx = new KanbanDbContext(Options);
        await ctx.Database.EnsureCreatedAsync();
    }
    
    public async Task DisposeAsync() => await _connection.DisposeAsync();
}


[CollectionDefinition("sqlite-db")]
public class SqliteDbCollection : ICollectionFixture<SqliteTestFixture>;
