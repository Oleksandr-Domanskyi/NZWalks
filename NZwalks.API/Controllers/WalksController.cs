using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.Dtos;
using NZwalks.API.Reposetories;

namespace NZwalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkReporitery walkReporitery;

        public WalksController(IMapper mapper, IWalkReporitery walkReporitery)
        {
            this.mapper = mapper;
            this.walkReporitery = walkReporitery;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWalkDto createWalkDto)
        {
            var walkDomainModel = mapper.Map<Walk>(createWalkDto);
            await walkReporitery.CreateAsync(walkDomainModel);
            return Ok(mapper.Map<WalksDto>(walkDomainModel));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           var walksDomainModel= await walkReporitery.GetALLAsync();
            return Ok(mapper.Map<List<WalksDto>>(walksDomainModel));
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetByID([FromRoute] Guid id)
        {
            var Rezult = await walkReporitery.GetByIDAsync(id);
            if (Rezult == null) { return NotFound(); }
            var RezultDto = mapper.Map<WalksDto>(Rezult);
            return Ok(RezultDto);
        }
        [HttpPut]
        [Route ("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody] UpdateWalksDtos updateWalksDtos)
        {
            var DomainToAppdate = mapper.Map<Walk>(updateWalksDtos);
            var WalksUpdate = await walkReporitery.UpdateAsync(id, DomainToAppdate);
            return Ok(mapper.Map<WalksDto>(WalksUpdate));
        }
        [HttpDelete]
        [Route ("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await walkReporitery.DeleteAsync(id));
        }
        
    }
}
