using Microsoft.AspNetCore.Identity;

namespace MVCDemoD06.Models
{
    public class ApplicationRole : IdentityRole
    {
        public string? Description { get; set; }
    }
}
