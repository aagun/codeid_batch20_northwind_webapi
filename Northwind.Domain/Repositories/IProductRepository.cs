

using Northwind.Domain.Entities;
using Northwind.Domain.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Domain.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> FindAllProduct();

        Task<IEnumerable<Product>> FindAllProductAsync();

        Task<IEnumerable<Product>> GetProductPaging(ProductParameters productParameters);

        Task<PagedList<Product>> GetProductPageList(ProductParameters productParameters);


        Product FindProductById(int productId);


        void Insert(Product product);

        void Edit(Product product);

        void Remove(Product product);

        int GetIdSequence();


    }
}
