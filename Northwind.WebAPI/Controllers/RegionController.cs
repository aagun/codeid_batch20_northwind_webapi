using Microsoft.AspNetCore.Mvc;
using Northwind.Contract.Models;
using Northwind.Domain.Base;
using Northwind.Domain.Entities;
using Northwind.Services.Abstraction;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Northwind.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public RegionController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            this._repositoryManager = repositoryManager;
            _logger = logger;
        }




        // GET: api/<RegionController>
        [HttpGet]
        public IActionResult Get()
        {
            var regions = _repositoryManager.RegionRepository.FindAllRegion().ToList();
            //throw new Exception("error");

            //use dto
            var regionDto = regions.Select(r => new RegionDto
            {
                RegionId = r.RegionId,
                RegionDescription=r.RegionDescription
            });

            return Ok(regionDto);

            /*var regions = _repositoryManager.RegionRepository.FindAllRegion().ToList();
            return Ok(regions);*/

            /*  try
              {
                  var regions = _repositoryManager.RegionRepository.FindAllRegion().ToList();

                  throw new Exception("error");

                  return Ok(regions);
              }
              catch (Exception)
              {
                  _logger.LogError($"Error : {nameof(Get)}");
                  return StatusCode(500, "Internal server error.");
                  //throw;
              }*/
        }

        // GET api/<RegionController>/5
        [HttpGet("{id}",Name ="GetRegion")]
        public IActionResult FindRegionById(int id)
        {
            var region = _repositoryManager.RegionRepository.FindRegionById(id);
            if(region == null)
            {
                _logger.LogError("Region object sent from client is null");
                return BadRequest("Region object is null");
            }

            var regionDto = new RegionDto
            {
                RegionId = region.RegionId,
                RegionDescription = region.RegionDescription
            };

            return Ok(regionDto);
        }

        // POST api/<RegionController>
        [HttpPost]
        public IActionResult CreateRegion([FromBody] RegionDto regionDto)
        {
            //1. prevent regiondto from null
            if (regionDto == null)
            {
                _logger.LogError("Regiondto object sent from client is null");
                return BadRequest("RegionDto object is null");
            }

            var region = new Region()
            {
                RegionId = regionDto.RegionId,
                RegionDescription = regionDto.RegionDescription
            };

            //post to db
            _repositoryManager.RegionRepository.Insert(region);

            //forward 
            return CreatedAtRoute("GetRegion", new { id = regionDto.RegionId }, regionDto);
        }

        // PUT api/<RegionController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateRegion(int id, [FromBody] RegionDto regionDto)
        {
            //1. prevent regiondto from null
            if (regionDto == null)
            {
                _logger.LogError("Regiondto object sent from client is null");
                return BadRequest("RegionDto object is null");
            }

            var region = new Region()
            {
                RegionId = id,
                RegionDescription = regionDto.RegionDescription
            };

            _repositoryManager.RegionRepository.Edit(region);

            //forward 
            return CreatedAtRoute("GetRegion", new { id = regionDto.RegionId }, new RegionDto { RegionId = id, RegionDescription = region.RegionDescription });

        }

        // DELETE api/<RegionController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            //1. prevent regiondto from null
            if (id == null)
            {
                _logger.LogError("Regiondto object sent from client is null");
                return BadRequest("RegionDto object is null");
            }

            //2. find id first
            var region = _repositoryManager.RegionRepository.FindRegionById(id.Value);
            if(region == null)
            {
                _logger.LogError($"Region with id {id} not found");
                return NotFound();
            }

            _repositoryManager.RegionRepository.Remove(region);
            return Ok("Data has been remove.");

        }
    }
}
