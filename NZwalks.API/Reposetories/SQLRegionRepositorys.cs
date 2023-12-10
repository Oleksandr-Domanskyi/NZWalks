using Microsoft.EntityFrameworkCore;
using NZwalks.API.Data;
using NZwalks.API.Models.Domain;

namespace NZwalks.API.Reposetories
{
    public class SQLRegionRepositorys : IRegionRepository
    {
        private readonly NZWalksDbContext _dbContext;

        public SQLRegionRepositorys(NZWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await _dbContext.Regions.AddAsync(region);
            await _dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var existingRegion = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            
            if(existingRegion is null) { return null; }
            _dbContext.Regions.Remove(existingRegion);
            await _dbContext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<List<Region>> GetAllAsync()
        {
           return await _dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
           return await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var exitingRegion = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (exitingRegion != null) { return null; }

            exitingRegion.Code= region.Code;
            exitingRegion.Name= region.Name;
            exitingRegion.ReoginImageURL = region.ReoginImageURL;
            
            await _dbContext.SaveChangesAsync();
            return exitingRegion;
        }
    }
}
