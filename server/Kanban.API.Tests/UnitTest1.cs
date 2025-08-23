using Kanban.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Kanban.API.Tests;

[Collection("sqlite-db")]
public class UnitTest1
{
    private readonly DbContextOptions<KanbanDbContext> _options;

    public UnitTest1(SqliteTestFixture fixture) => _options = fixture.Options;
    
    [Fact]
    public void Test1()
    {
        
    }
}