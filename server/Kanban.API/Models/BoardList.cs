using System.ComponentModel.DataAnnotations;

namespace Kanban.API.Models;

public class BoardList
{
    public Guid Id { get; set; }

    public Guid BoardId { get; set; }

    public required string Title { get; set; }

    public int Order { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public required string CreatedByUserId { get; set; }

    public ApplicationUser? CreatedByUser { get; set; }

    [Timestamp]
    public uint Version { get; set; }
}