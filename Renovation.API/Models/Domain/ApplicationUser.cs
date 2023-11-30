using Microsoft.AspNetCore.Identity;

namespace Renovation.API.Models.Domain
{
    public class ApplicationUser: IdentityUser
    {
        public UserProfile UserProfile { get; set; }
    }
}
