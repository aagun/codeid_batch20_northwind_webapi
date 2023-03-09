using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Northwind.Contract.Models;
using Northwind.Contracts.Models;
using Northwind.Domain.Base;
using Northwind.Domain.Entities;
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
            var products = await _repositoryManager.ProductRepository.FindAllProductAsync();
            return Ok(products);
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCreateDto productCreateDto)
        {

            if (productCreateDto == null)
            {
                _logger.LogError("Product object sent from client is null");
                return BadRequest("Product object is null");
            }

            var product = new Product
            {
                ProductName = productCreateDto.ProductName,
                CategoryID = productCreateDto.CategoryId.Value,
                SupplierID = productCreateDto.SupplierId.Value,
                QuantityPerUnit = productCreateDto.QuantityPerUnit,
                UnitPrice = (decimal)productCreateDto.UnitPrice,
                UnitsInStock = productCreateDto.UnitsInStock,
                UnitsOnOrder = productCreateDto.UnitsOnOrder,
                ReorderLevel = productCreateDto.ReorderLevel,
                Discontinued = productCreateDto.Discontinued,
            };


            //2. insert product to table
            _repositoryManager.ProductRepository.Insert(product);

            //3. if insert product success then get prorductId
            var productId = _repositoryManager.ProductRepository.GetIdSequence();

            // send as dto
            var productDto = new ProductDto
            {
                ProductID = productId,
                ProductName = productCreateDto.ProductName,
                CategoryID = productCreateDto.CategoryId.Value,
                SupplierID = productCreateDto.SupplierId.Value,
                QuantityPerUnit = productCreateDto.QuantityPerUnit ?? "",
                UnitPrice = (decimal)productCreateDto.UnitPrice,
                UnitsInStock = productCreateDto.UnitsInStock,
                UnitsOnOrder = productCreateDto.UnitsOnOrder,
                ReorderLevel = productCreateDto.ReorderLevel,
                Discontinued = productCreateDto.Discontinued,
            };

            //forward 
            return CreatedAtRoute("GetProduct", new { id = productId }, productDto);
  
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetProductPaging(int id)
        {
            var product =  _repositoryManager.ProductRepository.FindProductById(id);
            return Ok(product);
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

 
        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            //1. prevent productDto from null
            if (productDto == null)
            {
                _logger.LogError("productDto object sent from client is null");
                return BadRequest("productDto object is null");
            }

            var product = new Product()
            {
                ProductID = productDto.ProductID,
                ProductName = productDto.ProductName,
                CategoryID = productDto.CategoryID,
                SupplierID = productDto.SupplierID,
                QuantityPerUnit = productDto.QuantityPerUnit ?? "",
                UnitPrice = (decimal)productDto.UnitPrice,
                UnitsInStock = productDto.UnitsInStock,
                UnitsOnOrder = productDto.UnitsOnOrder,
                ReorderLevel = productDto.ReorderLevel,
                Discontinued = productDto.Discontinued,
            };

            _repositoryManager.ProductRepository.Edit(product);

            //forward 
            return CreatedAtRoute("GetProduct", new { id = productDto.ProductID }, productDto);

        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
