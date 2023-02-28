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
    internal class SupplierServices : ISupplierServices
    {
        private readonly IRepositoryManager _repositoryManager;

        public SupplierServices(IRepositoryManager repositoryManager)
        {
            this._repositoryManager = repositoryManager;
        }

        public void CreateSupplierProduct(SupplierProductDto supplierProductDto, out int supplierId)
        {
            //1. hold data from supplierProudctDto
            var supplierEntity = new Supplier
            {
                CompanyName = supplierProductDto.SupplierDto.CompanyName,
                Address = supplierProductDto.SupplierDto.Address
            };

            //2. insert into supplier
            _repositoryManager.SupplierRepository.Insert(supplierEntity);

            //3. get supplierId
            supplierId = _repositoryManager
                .SupplierRepository
                .GetSequenceId("SELECT IDENT_CURRENT('dbo.suppliers');");

            //4. hold data list product
            var products = supplierProductDto.ProductDtos;

            foreach (var item in products)
            {
                var product = new Product
                {
                    ProductName = item.ProductName,
                    CategoryID = item.CategoryId.Value,
                    SupplierID = supplierId,
                    Discontinued = false,
                    QuantityPerUnit = "per unit",
                    ReorderLevel = 10,
                    UnitPrice = 45_00,
                    UnitsInStock = 0,
                    UnitsOnOrder = 0
                    
                };

                _repositoryManager.ProductRepository.Insert(product);
            }

        }

     

    }
}
