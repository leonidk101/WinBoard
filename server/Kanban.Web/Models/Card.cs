using System.ComponentModel.DataAnnotations;

namespace Kanban.Web.Models;

public class Card
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [MaxLength(1000)]
    public string? Description { get; set; }
    
    public int Position { get; set; }
    
    public int ColumnId { get; set; }
    
    public Priority Priority { get; set; } = Priority.Medium;
    
    public DateTime? DueDate { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public Column Column { get; set; } = null!;
}

public enum Priority
{
    Low = 1,
    Medium = 2,
    High = 3,
    Critical = 4
}
