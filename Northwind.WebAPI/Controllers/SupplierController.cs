
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Northwind.Contracts.Models;
using Northwind.Domain.Base;
using Northwind.Domain.Entities;
using Northwind.Services.Abstraction;
using System.Data;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Northwind.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {

        private readonly ILoggerManager _logger;
        private IRepositoryManager _repositoryManager;
        private readonly IServiceManager serviceManager;


        public SupplierController(IRepositoryManager repositoryManager,
            ILoggerManager logger, IServiceManager serviceManager)
        {
            _repositoryManager = repositoryManager;
            this._logger = logger;
            this.serviceManager = serviceManager;
        }


        [HttpPost]
        public IActionResult CreateSupplierProduct([FromBody] SupplierProductDto supplierProductDto)
        {
            if (supplierProductDto != null)
            {
                serviceManager.SupplierService.CreateSupplierProduct(supplierProductDto, out var supplierId);

                return CreatedAtRoute("GetSupplierById", new { id = supplierId }, supplierProductDto);
            }
            return BadRequest();
        }


        [HttpGet("{id}", Name = "GetSupplierById")]
        public IActionResult GetSupplierById(int id)
        {
            var supplierProduct = _repositoryManager.SupplierRepository.GetSupplierProduct(id);
            return Ok(supplierProduct);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
