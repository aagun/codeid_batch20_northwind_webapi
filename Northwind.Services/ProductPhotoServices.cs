using Northwind.Contracts.Models;
using Northwind.Domain.Base;
using Northwind.Domain.Entities;
using Northwind.Domain.Repositories;
using Northwind.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services
{
    internal class ProductPhotoServices : IProductPhotoServices
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IUtilityService _utilityService;

        public ProductPhotoServices(IRepositoryManager repositoryManager, IUtilityService utilityService)
        {
            _repositoryManager = repositoryManager;
            _utilityService = utilityService;
        }

        public void InsertProductAndProductPhoto(ProductPhotoGroupDto productPhotoGroupDto, out int productId)
        {
            //1. insert into table product
            var product = new Product
            {
                ProductName = productPhotoGroupDto.ProductForCreateDto.ProductName,
                CategoryID = (int)productPhotoGroupDto.ProductForCreateDto.CategoryId,
                SupplierID = (int)productPhotoGroupDto.ProductForCreateDto.SupplierId,
                QuantityPerUnit = productPhotoGroupDto.ProductForCreateDto.QuantityPerUnit,
                UnitPrice = (decimal)productPhotoGroupDto.ProductForCreateDto.UnitPrice,
                UnitsInStock = productPhotoGroupDto.ProductForCreateDto.UnitsInStock,
                UnitsOnOrder = productPhotoGroupDto.ProductForCreateDto.UnitsOnOrder,
                ReorderLevel = productPhotoGroupDto.ProductForCreateDto.ReorderLevel,
                Discontinued = productPhotoGroupDto.ProductForCreateDto.Discontinued,
            };


            //2. insert product to table
            _repositoryManager.ProductRepository.Insert(product);
            
            //3. if insert product success then get prorductId
            productId = _repositoryManager.ProductRepository.GetIdSequence();


            //4. extract photos 
            var allPhotos = productPhotoGroupDto.AllPhotos;


            foreach (var itemPhoto in allPhotos)
            {
                var fileName = _utilityService.UploadSingleFile(itemPhoto);
                var productPhoto = new ProductPhoto
                {
                    PhotoFilename = fileName,
                    PhotoFileSize = (short?)itemPhoto.Length,
                    PhotoFileType = itemPhoto.ContentType,
                    PhotoOriginalFilename = itemPhoto.FileName,
                    PhotoPrimary = 0,
                    PhotoProductId = productId
                };
                _repositoryManager.ProductPhotoRepository.Insert(productPhoto);
            }

           


        }
    }
}
