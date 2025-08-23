using System.ComponentModel.DataAnnotations;

namespace Kanban.API.Models;

public class TaskItem
{
    public Guid Id { get; set; }

    public Guid BoardListId { get; set; }

    public required string Title { get; set; }

    public string? Description { get; set; }

    public int Order { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public required string CreatedByUserId { get; set; }

    [Timestamp]
    public uint Version { get; set; }
}