using Microsoft.EntityFrameworkCore;
using Renovation.API.Data;
using Renovation.API.Models.Domain;

namespace Renovation.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly RenovationDbContext dbContext;
        public SQLRegionRepository(RenovationDbContext dbContext)
        {
            this.dbContext=dbContext;
            
        }
        public async Task<List<Region>> GetAllAsync(string? filterOn = null, string? filterQuery = null, 
            string? sortBy=null, bool isAscending=true, int pageNumber = 1, int pageSize = 1)
        {
            var regions = dbContext.Regions.AsQueryable();

            //Filtering
            if(string.IsNullOrWhiteSpace(filterOn)== false && string.IsNullOrEmpty(filterQuery) == false)
            {
                if(filterOn.Equals("Name",StringComparison.OrdinalIgnoreCase) )
                {
                    regions = regions.Where(x => x.Name.Contains(filterQuery));
                }
            }

            //Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Name",StringComparison.OrdinalIgnoreCase))
                {
                    regions = isAscending ? regions.OrderBy(x=>x.Name): regions.OrderByDescending(x=>x.Name);
                }
                else if (sortBy.Equals("Code", StringComparison.OrdinalIgnoreCase))
                {
                    regions = isAscending ? regions.OrderBy(x => x.Code) : regions.OrderByDescending(x => x.Code);
                }
            }

            //Pgination
            var skipResults = (pageNumber-1)*pageSize;

            return await regions.Skip(skipResults).Take(pageSize).ToListAsync();
            //return await dbContext.Regions.ToListAsync();

        }
        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existing = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return null;
            existing.Code = region.Code;
            existing.Name = region.Name;
            existing.RegionImageUrl = region.RegionImageUrl;
            await dbContext.SaveChangesAsync();
    
            return existing;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existing = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return null;
            dbContext.Regions.Remove(existing);
            await dbContext.SaveChangesAsync();
            return existing;
        }
    }
}
