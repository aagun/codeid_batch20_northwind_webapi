using Northwind.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services.Abstraction
{
    public interface ISupplierServices
    {
        public void CreateSupplierProduct(SupplierProductDto supplierProductDto,out int supplierId);


    }
}
