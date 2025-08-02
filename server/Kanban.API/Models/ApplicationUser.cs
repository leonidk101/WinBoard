using Microsoft.AspNetCore.Identity;

namespace Kanban.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Board> Boards { get; set; } = new List<Board>();
    }
}