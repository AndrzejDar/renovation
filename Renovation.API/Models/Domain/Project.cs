namespace Renovation.API.Models.Domain
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string User { get; set; }
        public string City {  get; set; }
        public Guid RoomId {  get; set; }
        public Guid RegionIs {  get; set; }

        //Navigation properties
        public Room Rooms { get; set; }
        public Region Region { get; set; }
    }
}
