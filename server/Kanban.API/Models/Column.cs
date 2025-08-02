using System.ComponentModel.DataAnnotations;

namespace Kanban.API.Models;

public class Column
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    public int Position { get; set; }
    
    public int BoardId { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public Board Board { get; set; } = null!;
    public ICollection<Card> Cards { get; set; } = new List<Card>();
}
