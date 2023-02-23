
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
    internal class ProductPhotoRepository : RepositoryBase<ProductPhoto>, IProductPhotoRepository
    {
        public ProductPhotoRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

    

        public void Edit(ProductPhoto productPhoto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductPhoto>> FindAllOrderAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductPhoto> FindAllProduct()
        {
            IEnumerator<ProductPhoto> dataSet = FindAll<ProductPhoto>("SELECT * FROM dbo.ProductPhotos");
 
            while (dataSet.MoveNext())
            {
                var item = dataSet.Current;
                yield return item;

            }
        }


        public async Task<IEnumerable<ProductPhoto>> FindAllProductAsync()
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM DBO.ProductPhotos;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] { }

            };

            IAsyncEnumerator<ProductPhoto> dataSet =  FindAllAsync<ProductPhoto>(model);

            var item = new List<ProductPhoto>();


             while (await dataSet.MoveNextAsync())
              {
                item.Add(dataSet.Current);
              }


            return item;

        }

        public IEnumerable<ProductPhoto> FindAllProductPhoto()
        {
            throw new NotImplementedException();
        }

        public ProductPhoto FindOrderById(int id)
        {
            throw new NotImplementedException();
        }

        

        

        public Product FindProductPhotoById(int productId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM DBO.ProductPhotos where ProductID=@productId;",
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





        public void Insert(ProductPhoto productPhoto)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = @"INSERT INTO productphotos 
                (PhotoFilename,PhotoFileSize,PhotoFileType,PhotoProductId,PhotoPrimary,PhotoOriginalFilename) 
                values (@PhotoFilename,@PhotoFileSize,@PhotoFileType,@PhotoProductId,@PhotoPrimary,@PhotoOriginalFilename);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@PhotoFilename",
                        DataType = DbType.String,
                        Value = productPhoto.PhotoFilename
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@PhotoFileSize",
                        DataType = DbType.Int64,
                        Value = productPhoto.PhotoFileSize
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@PhotoFileType",
                        DataType = DbType.String,
                        Value = productPhoto.PhotoFileType
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@PhotoProductId",
                        DataType = DbType.Int64,
                        Value = productPhoto.PhotoProductId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@PhotoPrimary",
                        DataType = DbType.Int64,
                        Value = productPhoto.PhotoPrimary
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@PhotoOriginalFilename",
                        DataType = DbType.String,
                        Value = productPhoto.PhotoOriginalFilename
                    },
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }



        public void Remove(ProductPhoto productPhoto)
        {
            throw new NotImplementedException();
        }
    }
}
