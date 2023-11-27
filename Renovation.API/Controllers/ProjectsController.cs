using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Renovation.API.Data;
using Renovation.API.Models.Domain;
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

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var project = await projectRepository.GetByIdAsync(id);
            if(project == null) return NotFound(); 

            return Ok(mapper.Map<ProjectDto>(project));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AddProjectRequestDto addProjectRequestDto)
        {
            var project = mapper.Map<Project>(addProjectRequestDto);
            project = await projectRepository.CreateAsync(project);

            var projectDto = mapper.Map<ProjectDto>(project);
            //return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
            return CreatedAtAction(nameof(GetById), new { id = projectDto.Id }, projectDto);
            //return Ok(projectDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProjectRequestDto projectRequestDto)
        {
            var project = mapper.Map<Project>(projectRequestDto); 
            project= await projectRepository.UpdateAsync(id, project);
            if(project == null) return NotFound();

            return Ok(mapper.Map<ProjectDto>(project));
        }
    }
}
