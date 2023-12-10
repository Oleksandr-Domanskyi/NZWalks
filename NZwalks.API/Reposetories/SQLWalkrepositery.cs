using Microsoft.EntityFrameworkCore;
using NZwalks.API.Data;
using NZwalks.API.Models.Domain;
using System.Globalization;

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

        public async Task<List<Walk>> GetALLAsync(string? filterOn = null, string? filterQuery = null,
            string?  sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var walks = _dbContext.Walks.Include(x => x.Difficulty).Include(x => x.Region).AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }
            //Sorting
            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if(sortBy.Equals("Lenght", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LenghtInKm) : walks.OrderByDescending(x => x.LenghtInKm);
                }
            }
            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> GetByIDAsync(Guid id)
        {
          return  await _dbContext.Walks.Include(x => x.Difficulty).Include(x => x.Region).FirstOrDefaultAsync(x => x.Id == id);

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
