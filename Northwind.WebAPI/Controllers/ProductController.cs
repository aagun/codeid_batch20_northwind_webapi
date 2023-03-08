using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Northwind.Contracts.Models;
using Northwind.Domain.Base;
using Northwind.Domain.RequestFeatures;
using Northwind.Services.Abstraction;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Northwind.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly ILoggerManager _logger;
        private IRepositoryManager _repositoryManager;
        private readonly IServiceManager _serviceManager;

        public ProductController(ILoggerManager logger, IRepositoryManager repositoryManager, IServiceManager serviceManager)
        {
            _logger = logger;
            _repositoryManager = repositoryManager;
            _serviceManager = serviceManager;
        }



        // GET: api/<ProductController>
        //[HttpGet, Authorize(Roles ="Administrator,Manager")]
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var products =  await _repositoryManager.ProductRepository.FindAllProductAsync();
            return Ok(products);
        }


        [HttpPost("createProductAndUpload"), DisableRequestSizeLimit]
        public async Task<IActionResult> CreateProductPhoto()
        {
            //1. declare formCollection to hold form-data
            var formColletion = await Request.ReadFormAsync();

            //2. extract files to variable files
            var files = formColletion.Files;

            //3. hold each ouput formCollection to each variable
            formColletion.TryGetValue("ProductName", out var productName);
            formColletion.TryGetValue("SupplierId", out var supplierId);
            formColletion.TryGetValue("CategoryId", out var categoryId);
            formColletion.TryGetValue("QuantityPerUnit", out var quantityPerUnit);
            formColletion.TryGetValue("UnitPrice", out var unitPrice);
            formColletion.TryGetValue("UnitsOnOrder", out var unitsOnOrder);
            formColletion.TryGetValue("ReorderLevel", out var reorderLevel);
            formColletion.TryGetValue("Discontinued", out var discontinued);

            //4. declare variable and store in object 
            var productCreateDto = new ProductCreateDto
            {
                ProductName = productName.ToString(),
                SupplierId = int.Parse(supplierId.ToString()),
                CategoryId = int.Parse(categoryId.ToString()),
                QuantityPerUnit = quantityPerUnit.ToString(),
                UnitPrice = decimal.Parse(unitPrice.ToString()),
                UnitsOnOrder = short.Parse(unitsOnOrder.ToString()),
                ReorderLevel = short.Parse(reorderLevel.ToString()),
                Discontinued = bool.Parse(discontinued.ToString())
            };

            //5. store to list
            var allPhotos = new List<IFormFile>();
            foreach (var item in files)
            {
                allPhotos.Add(item);
            }

            //6. declare variable productphotogroup
            var productPhotoGroup = new ProductPhotoGroupDto
            {
                ProductForCreateDto = productCreateDto,
                AllPhotos = allPhotos
            };

            if (productPhotoGroup != null)
            {
                _serviceManager.ProductPhotoService.InsertProductAndProductPhoto(productPhotoGroup, out var productId);
                var productResult = _repositoryManager.ProductRepository.FindProductById(productId);
                return Ok(productResult);
            }

            return BadRequest();
        }


     
        [HttpGet("paging")]
        public async Task<IActionResult> GetProductPaging ([FromQuery] ProductParameters productParameters)
        {
            var products = await _repositoryManager.ProductRepository.GetProductPaging(productParameters);
            return Ok(products);
        }

        [HttpGet("pageList")]
        public async Task<IActionResult> GetProductPageList([FromQuery] ProductParameters productParameters)
        {
            if (!productParameters.ValidateStockRange)
                return BadRequest("MaxStock must greater than MinStock");
            
            var products = await _repositoryManager.ProductRepository.GetProductPageList(productParameters);


            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(products.MetaData));

            return Ok(products);
        }

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
