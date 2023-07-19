using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data From Database -Domain Models
            //var regionsDomain = await dbContext.Regions.ToListAsync();

            var regionsDomain = await regionRepository.GetAllAsync();
            //Map Domain Models to DTOs
            //var regionsDto = new List<RegionDto>();
            //foreach (var regionDomain in regionsDomain)
            //{
            //    regionsDto.Add(new RegionDto()
            //    {
            //        Id = regionDomain.Id,
            //        Name = regionDomain.Name,
            //        Code = regionDomain.Code,
            //        RegionImageUrl = regionDomain.RegionImageUrl
            //    });
            //}

           // var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);


            //Return DTOs to Client
            //return Ok(regionsDto);
            return Ok(mapper.Map<List<RegionDto>>(regionsDomain));

        }

        //get single region(get region by id)
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)//because ur id is guid
        {
            //var region = dbContext.regions.Find(id);



            //we can use FirstOrDefault instead of .Find so wecan pass any id we want if we add Find(id) we must pass id what is there in db
            //find method only takes primary key
            //Get Region Domain model from Databaes
            //var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            var regionDomain = await regionRepository.GetByIdAsync(id);
            // in this method we can use code name anything is there it must there in route also
            if (regionDomain == null)
            {
                return NotFound();
            }

            //map or convert domain model to region dto
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomain.Id,
            //    Name = regionDomain.Name,
            //    Code = regionDomain.Code,
            //    RegionImageUrl = regionDomain.RegionImageUrl
            //};

            //return DTO back to client
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map or Conver DTO to Domain Model
            //var regionDomainModel = new Region
            //{
            //    Code = addRegionRequestDto.Code,
            //    Name = addRegionRequestDto.Name,
            //    RegionImageUrl = addRegionRequestDto.RegionImageUrl
            //};

            var regionDomainModel = mapper.Map<Region> (addRegionRequestDto);

            // Use Domain Model to Create Region
            //await dbContext.Regions.AddAsync(regionDomainModel);
            //await dbContext.SaveChangesAsync();

            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            // Map Domain model back to DTO
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        // Update region
        // PUT
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDtocs updateRegionRequestDtocs)
        {

            //Map DTO to Domain Model
            //var regionDomainModel = new Region

            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDtocs);
            //{
            //    Code = updateRegionRequestDtocs.Code,
            //    Name = updateRegionRequestDtocs.Name,
            //    RegionImageUrl = updateRegionRequestDtocs.RegionImageUrl
            //};
            // Check  if region exists
            //var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            regionDomainModel =  await regionRepository.UpdateAsync(id, regionDomainModel);

            if(regionDomainModel == null)
            {
                return NotFound();
            }

            // Map DTO to Domain model
            //regionDomainModel.Code = updateRegionRequestDtocs.Code;
            //regionDomainModel.Name = updateRegionRequestDtocs.Name;
            //regionDomainModel.RegionImageUrl = updateRegionRequestDtocs.RegionImageUrl;

            //await dbContext.SaveChangesAsync();

            // Conver Domain Model to DTO
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};
            //var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            //return Ok(regionDto);
            return Ok(mapper.Map<RegionDto>(regionDomainModel));

        }

        // Delete Region
        // DELETE:
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Delete region
            //dbContext.Regions.Remove(regionDomainModel); // remove doesn't have async method
            //await dbContext.SaveChangesAsync();

            // return deleted Region back
            // Map Domain Model to DTO
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};

 
            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }
    }
}
