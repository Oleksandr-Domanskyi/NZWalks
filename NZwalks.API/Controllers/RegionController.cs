using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Data;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.Dtos;
using NZwalks.API.Models.Dtos.CreateRegionDto;

namespace NZwalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;
        private readonly IMapper _mapper;

        public RegionController(NZWalksDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
           //Get Data From Database - Domain models
           var regionsDomain = _dbContext.Regions.ToList();
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
        public IActionResult GetById(Guid id) 
        {
            //var region = _dbContext.Regions.Find(id);//tylko dla id
            var regionDomain = _dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            //Map/Convert Region Domain Model to Region DTO

            var regionsDto = _mapper.Map<RegionDto>(regionDomain);
            return Ok(regionsDto);  
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var rezult = _mapper.Map<Region>(addRegionRequestDto);
            _dbContext.Add(rezult);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new {id = rezult.Id }, rezult);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute]Guid id, [FromBody] UpdateRegionRequestDto dto)
        {
            var RegiomDomain = _dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if(RegiomDomain == null)
            {
                return NotFound();
            }
            RegiomDomain.Code = dto.Code;
            RegiomDomain.Name = dto.Name;
            RegiomDomain.ReoginImageURL = dto.ReoginImageURL;
            _dbContext.SaveChanges();

            var regionsDto = _mapper.Map<RegionDto>(RegiomDomain);
            

            return Ok(regionsDto);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute]Guid id)
        {
            var RegiomDomain = _dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (RegiomDomain == null)
            {
                return NotFound();
            }
            _dbContext.Regions.Remove(RegiomDomain);
            _dbContext.SaveChanges();

            var regionsDto = _mapper.Map<RegionDto>(RegiomDomain);

            return Ok(regionsDto);
        }
    }
}
