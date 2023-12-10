using Microsoft.EntityFrameworkCore;
using NZwalks.API.Data;
using NZwalks.API.Models.Domain;

namespace NZwalks.API.Reposetories
{
    public class SQLWalkrepositery : IWalkReporitery
    {
        private readonly NZWalksDbContext _dbContext;

        public SQLWalkrepositery(NZWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await _dbContext.Walks.AddAsync(walk);
            await _dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetALLAsync()
        {
           return await _dbContext.Walks.ToListAsync();
            
        }

        public async Task<Walk?> GetByIDAsync(Guid id)
        {
          return  await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Walk> UpdateAsync(Guid id ,Walk updatewalk)
        {
            var WalksDomain = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (WalksDomain == null) { return null; }
            
            WalksDomain.Name=updatewalk.Name;
            WalksDomain.DifficultyId = updatewalk.DifficultyId;
            WalksDomain.RegionId = updatewalk.RegionId;
            WalksDomain.LenghtInKm=updatewalk.LenghtInKm;
            WalksDomain.WalkImageUrl = updatewalk.WalkImageUrl;
            WalksDomain.Description = updatewalk.Description;
            

            await _dbContext.SaveChangesAsync();
            return WalksDomain;
        }
        public async Task<Walk> DeleteAsync(Guid id)
        {
            var walksDelete = await _dbContext.Walks.FirstOrDefaultAsync(x=>x.Id== id);
            if (walksDelete == null) { return null; }

            _dbContext.Walks.Remove(walksDelete);
            _dbContext.SaveChanges();
            return walksDelete;
        }
    }
}
