using Renovation.API.Models.Domain;

namespace Renovation.API.Models.DTO
{
    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string User { get; set; }
        public string City { get; set; }
        public Guid RoomId { get; set; }
        public Guid RegionId { get; set; }

        //Navigation properties
        public Room Rooms { get; set; }
        public Region Region { get; set; }
    }
}
