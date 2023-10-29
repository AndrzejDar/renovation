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
        public DbSet<RoomType> RoomTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed data
            //Room types
            var roomTypes = new List<RoomType>()
            {
                new RoomType()
                {
                    Id=Guid.Parse("0f7dd051-61a2-4dae-960a-bd526526351f"),
                    Name="Toilet"
                },
                new RoomType()
                {
                    Id=Guid.Parse("557b823d-d64f-4178-b187-4f05759b1d5f"),
                    Name="Bedroom"
                },
                new RoomType()
                {
                    Id=Guid.Parse("0b0b7f6b-604e-44aa-a33a-3e8da049e416"),
                    Name="Kitchen"
                },
                new RoomType()
                {
                    Id=Guid.Parse("b0eb59b9-7519-4fd2-9b6f-b3cf88ca820a"),
                    Name="LivingRoom"
                },
            };
            //Seed room types to db
            modelBuilder.Entity<RoomType>().HasData(roomTypes);
        }   
        
    }
}
