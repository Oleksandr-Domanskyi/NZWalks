using NZwalks.API.Models.Domain;
using NZwalks.API.Models.Dtos;

namespace NZwalks.API.Reposetories
{
    public interface IWalkReporitery
    {
        Task<Walk>CreateAsync(Walk walk);
        Task<List<Walk>>GetALLAsync();
        Task<Walk>UpdateAsync(Guid id, Walk walk);
        Task<Walk?>GetByIDAsync(Guid id);
        Task<Walk> DeleteAsync(Guid id);
    }
}
