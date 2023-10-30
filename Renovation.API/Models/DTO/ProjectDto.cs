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

        //Navigation properties
        public Room Rooms { get; set; }
        public RegionDto Region { get; set; }
    }

    public class AddProjectRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string User { get; set; }
        public string City { get; set; }
    }  
    public class UpdateProjectRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
    }
}
