using Renovation.API.Models.Domain;

namespace Renovation.API.Repositories
{
        public interface IProjectRepository
        {
            Task<List<Project>> GetAllAsync();
            //Task<Project?> GetByIdAsync(Guid id);
            //Task<Project> CreateAsync(Project region);
            //Task<Project?> UpdateAsync(Guid id, Project region);
            //Task<Project?> DeleteAsync(Guid id);
        }
    
}
