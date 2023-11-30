using Microsoft.AspNetCore.Identity;

namespace Renovation.API.Models.Domain
{
    public class UserProfile
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }

        //Foreign key to lintk to the AspNetUsersTable
        public string ApplicationUserId { get; set; }

        //Navigation property
        public ApplicationUser ApplicationUser { get; set; }
    }
}
