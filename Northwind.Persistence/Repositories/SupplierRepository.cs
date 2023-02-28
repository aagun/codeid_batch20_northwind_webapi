
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

    internal class SupplierRepository : RepositoryBase<Supplier>, ISupplierRepository
    {
        public SupplierRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }



        public IEnumerable<Supplier> FindAllSupplier()
        {
            IEnumerator<Supplier> dataSet = FindAll<Supplier>("SELECT * FROM Suppliers");

            while (dataSet.MoveNext())
            {
                var item = dataSet.Current;
                yield return item;

            }
        }


        public async Task<IEnumerable<Supplier>> FindAllSupplierAsync()
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM Suppliers;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] { }

            };

            IAsyncEnumerator<Supplier> dataSet = FindAllAsync<Supplier>(model);

            var item = new List<Supplier>();


            while (await dataSet.MoveNextAsync())
            {
                item.Add(dataSet.Current);
            }


            return item;

        }



        public Supplier FindSupplierById(int SupplierId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM Suppliers where SupplierId=@SupplierId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@SupplierId",
                        DataType = DbType.Int32,
                        Value = SupplierId
                    }
                }
            };

            var dataSet = FindByCondition<Supplier>(model);

            var item = new Supplier();

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }



            return item;

        }



        public void Insert(Supplier supplier)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = @"INSERT INTO suppliers 
                (CompanyName,Address) 
                values (@CompanyName,@Address);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@CompanyName",
                        DataType = DbType.String,
                        Value = supplier.CompanyName
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@Address",
                        DataType = DbType.String,
                        Value = supplier.Address
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public void Remove(Supplier supplier)
        {
            throw new NotImplementedException();
        }



        public void Edit(Supplier supplier)
        {
            throw new NotImplementedException();
        }

        public int GetSequenceId(string sql)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = sql,
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] { }
            };

            var id = _adoContext.ExecuteScalar<decimal>(model);
            _adoContext.Dispose();

            return (int)id;
        }

        public SupplierNestedProduct GetSupplierProduct(int supplierId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = @"select s.SupplierID,CompanyName,Address, 
                                p.ProductID,ProductName,CategoryID,QuantityPerUnit,UnitPrice,
                                UnitsInStock,UnitsOnOrder,Discontinued,ReorderLevel
                                from Suppliers s 
                                join Products p on s.SupplierID=p.SupplierID
                                where s.SupplierID=@supplierId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                 new SqlCommandParameterModel() {
                        ParameterName = "@supplierId",
                        DataType = DbType.Int32,
                        Value = supplierId
                    }
                }
            };

            var dataSet = FindByCondition<SupplierJoinProduct>(model);

            var listData = new List<SupplierJoinProduct>();

            while (dataSet.MoveNext())
            {
                listData.Add(dataSet.Current);
            }

            var supplier = listData.Select(x => new { x.SupplierID, x.CompanyName, x.Address }).FirstOrDefault();

            var products = listData.Select(x => new Product
            {
                SupplierID = x.SupplierID,
                CategoryID = x.CategoryID,
                Discontinued = x.Discontinued,
                ProductID = x.ProductID,
                QuantityPerUnit = x.QuantityPerUnit,
                ReorderLevel = x.ReorderLevel,
                UnitPrice = x.UnitPrice,
                UnitsInStock = x.UnitsInStock,
                UnitsOnOrder = x.UnitsOnOrder
            });

            var nestedJson = new SupplierNestedProduct
            {
                SupplierID = supplier.SupplierID,
                CompanyName = supplier.CompanyName,
                Address = supplier.Address,
                Products = products.ToList()
            };

            return nestedJson;
        }
    }
}
