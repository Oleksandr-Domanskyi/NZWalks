using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZwalks.API.Data;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.Dtos;
using NZwalks.API.Models.Dtos.CreateRegionDto;
using NZwalks.API.Reposetories;

namespace NZwalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IRegionRepository regionRepository;

        public RegionController(NZWalksDbContext dbContext, IMapper mapper, IRegionRepository regionRepository)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            this.regionRepository = regionRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           //Get Data From Database - Domain models
           var regionsDomain = await regionRepository.GetAllAsync();
            //Map Domain Models to DTOs
           var regionsDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    ReoginImageURL = regionDomain.ReoginImageURL,
                });
            }
            //Returns DTOs
           return Ok(regionsDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) 
        {
            //var region = _dbContext.Regions.Find(id);//tylko dla id
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            //Map/Convert Region Domain Model to Region DTO

            var regionsDto = _mapper.Map<RegionDto>(regionDomain);
            return Ok(regionsDto);  
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var rezult = _mapper.Map<Region>(addRegionRequestDto);
            await regionRepository.CreateAsync(rezult);

            return CreatedAtAction(nameof(GetById), new {id = rezult.Id }, rezult);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody] UpdateRegionRequestDto dto)
        {
            var RegiomDomain = new Region
            {
                Code = dto.Code,
                Name = dto.Name,
                ReoginImageURL = dto.ReoginImageURL,
            };
            RegiomDomain = await regionRepository.UpdateAsync(id, RegiomDomain);
            if(RegiomDomain == null)
            {
                return NotFound();
            }

            var regionsDto = _mapper.Map<RegionDto>(RegiomDomain);
            

            return Ok(regionsDto);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var RegiomDomain = await regionRepository.DeleteAsync(id);
            if (RegiomDomain == null)
            {
                return NotFound();
            }

            var regionsDto = _mapper.Map<RegionDto>(RegiomDomain);

            return Ok(regionsDto);
        }
    }
}
