using System.ComponentModel.DataAnnotations;

namespace Kanban.API.Models;

public class Board
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public ApplicationUser? CreatedByUser { get; set; }

    public required string CreatedByUserId { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    [Timestamp]
    public uint Version { get; set; }
}
