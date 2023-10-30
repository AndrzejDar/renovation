using AutoMapper;
using Renovation.API.Models.Domain;
using Renovation.API.Models.DTO;

namespace Renovation.API.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            //PROJECTS
            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<Project, AddProjectRequestDto>().ReverseMap();
            CreateMap<Project, UpdateProjectRequestDto>().ReverseMap();
            //REGIONS
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
        }
    }
}
