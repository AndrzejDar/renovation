using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Renovation.API.Data
{
    public class RenovationAuthDbContext : IdentityDbContext
    {
        public RenovationAuthDbContext(DbContextOptions<RenovationAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "d44ca867-406b-44a5-88ac-f1ab46c21448";
            var writerRoleId = "d4bca13c-ab4e-48bd-b028-4fac8895dbdc";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id= readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name= "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id= writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name= "Writer",
                    NormalizedName = "Writer".ToUpper()
                },
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
    
