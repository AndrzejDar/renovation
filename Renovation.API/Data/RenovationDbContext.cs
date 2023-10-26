using Microsoft.EntityFrameworkCore;
using Renovation.API.Models.Domain;

namespace Renovation.API.Data
{
    public class RenovationDbContext: DbContext
    {
        public RenovationDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
               
        }
        public DbSet<RenovationTask> RenovationTasks { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Project> Projects { get; set; }
        
    }
}
