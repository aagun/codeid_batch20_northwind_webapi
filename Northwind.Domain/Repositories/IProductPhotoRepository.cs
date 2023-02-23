


using Northwind.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Domain.Repositories
{
    public interface IProductPhotoRepository
    {
        IEnumerable<ProductPhoto> FindAllProductPhoto();
        Task<IEnumerable<ProductPhoto>> FindAllOrderAsync();
        ProductPhoto FindOrderById(int id);

        void Insert(ProductPhoto productPhoto);

        void Edit(ProductPhoto productPhoto);

        void Remove(ProductPhoto productPhoto);

    }
}
