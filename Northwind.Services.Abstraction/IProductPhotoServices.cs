using Northwind.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services.Abstraction
{
    public interface IProductPhotoServices
    {
        public void InsertProductAndProductPhoto(ProductPhotoGroupDto productPhotoGroupDto,out int productId);
    }
}
