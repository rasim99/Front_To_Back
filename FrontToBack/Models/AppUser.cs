using Microsoft.AspNetCore.Identity;

namespace FrontToBack.Models
{
    public class AppUser : IdentityUser
    {
       
        public string FullName { get; set; }
        public bool IsBlock { get; set; }
        public string? ConnectionId { get; set; }
    }
}
