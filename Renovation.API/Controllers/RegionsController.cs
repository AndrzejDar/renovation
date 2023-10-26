using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Renovation.API.Data;
using Renovation.API.Models.Domain;
using Renovation.API.Models.DTO;

namespace Renovation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly RenovationDbContext dbContext;
        public RegionsController (RenovationDbContext dbContext)
        {
            this.dbContext = dbContext;           
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            //Domain models
            var regions = dbContext.Regions.ToList();

            //Map DM to DTOs
            var regionsDto = new List<RegionDto>();
            foreach(var region in regions)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl,
                    Code = region.Code,
                });
            }
            //Return DTO
            return Ok(regionsDto);

        }        
        
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id);     
            var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if(region == null)return NotFound();
            var regionDto = new RegionDto { 
                Code=region.Code,
                Id=region.Id,
                Name=region.Name,
                RegionImageUrl=region.RegionImageUrl,
            };
            return Ok(regionDto);
        }

        [HttpPost]
        
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //map DTO to Domain model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            //Map Domanin model back to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }
    }
}
