using AutoMapper;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.Dtos;
using NZwalks.API.Models.Dtos.CreateRegionDto;

namespace NZwalks.API.Mapping
{
    public class NZWalkingMappingcs : Profile
    {
        public NZWalkingMappingcs()
        {
            CreateMap<Region, RegionDto>();
            CreateMap<AddRegionRequestDto, Region>();
        }
    }
}
