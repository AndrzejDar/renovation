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
            return await dbContext.Projects.ToListAsync();
        }
    }
}
