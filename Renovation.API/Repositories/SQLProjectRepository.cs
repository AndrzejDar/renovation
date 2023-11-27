using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Renovation.API.Data;
using Renovation.API.Models.Domain;

namespace Renovation.API.Repositories
{
    public class SQLProjectRepository: IProjectRepository
    {
        private readonly RenovationDbContext dbContext;
        public SQLProjectRepository(RenovationDbContext dbContext) 
        { 
            this.dbContext = dbContext;           
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await dbContext.Projects.Include(x=>x.Region).ToListAsync();
        }

        public async Task<Project?> GetByIdAsync(Guid id)
        {
            return await dbContext.Projects.FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Project> CreateAsync(Project project)
        {
            await dbContext.Projects.AddAsync(project);
            await dbContext.SaveChangesAsync();
            return project;
        }

        public async Task<Project?> UpdateAsync(Guid id, Project project)
        {
            var existing = await dbContext.Projects.FirstOrDefaultAsync(x=>x.Id==id);
            if (existing == null) return null;

            existing.Name = project.Name;
            existing.Description = project.Description; 
            existing.City = project.City; 

            await dbContext.SaveChangesAsync();
            return existing;
        }
    }
}
