using Northwind.Domain.Entities;
using Northwind.Domain.Repositories;
using Northwind.Persistence.Base;

using Northwind.Persistence.RepositoryContext;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Persistence.Repositories
{
    internal class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(Product product)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> FindAllProduct()
        {
            IEnumerator<Product> dataSet = FindAll<Product>("SELECT * FROM dbo.Products");
 
            while (dataSet.MoveNext())
            {
                var item = dataSet.Current;
                yield return item;

            }
        }


        public async Task<IEnumerable<Product>> FindAllProductAsync()
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM DBO.Products;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] { }

            };

            IAsyncEnumerator<Product> dataSet =  FindAllAsync<Product>(model);

            var item = new List<Product>();


             while (await dataSet.MoveNextAsync())
              {
                item.Add(dataSet.Current);
              }


            return item;

        }


        public Product FindProductById(int productId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM DBO.Products where ProductID=@productId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@productId",
                        DataType = DbType.Int32,
                        Value = productId
                    }
                }
            };

            var dataSet = FindByCondition<Product>(model);

            var item = new Product();

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }
            return item;

        }



        public void Insert(Product product)
        {
            throw new NotImplementedException();
        }

        public void Remove(Product product)
        {
            throw new NotImplementedException();
        }



      
    }
}
