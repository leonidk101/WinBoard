using System.ComponentModel.DataAnnotations;

namespace Kanban.API.Models;

public class Board
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? CreatedById { get; set; }

    public ApplicationUser? CreatedBy { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
