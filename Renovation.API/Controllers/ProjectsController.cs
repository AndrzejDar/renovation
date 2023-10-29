using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Renovation.API.Data;
using Renovation.API.Models.DTO;
using Renovation.API.Repositories;

namespace Renovation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly RenovationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IProjectRepository projectRepository;
  
        public ProjectsController(RenovationDbContext dbContext, IMapper mapper, IProjectRepository projectRepository) 
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.projectRepository = projectRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var projects = await projectRepository.GetAllAsync();
            return Ok(mapper.Map<List<ProjectDto>>(projects));
        }
    }
}
