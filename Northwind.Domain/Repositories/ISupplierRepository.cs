


using Northwind.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Domain.Repositories
{
    public interface ISupplierRepository
    {
        IEnumerable<Supplier> FindAllSupplier();

        Task<IEnumerable<Supplier>> FindAllSupplierAsync();


        Supplier FindSupplierById(int supplierId);

        SupplierNestedProduct GetSupplierProduct(int supplierId);


        int GetSequenceId(string sql);


        void Insert(Supplier supplier);

        void Edit(Supplier supplier);

        void Remove(Supplier supplier);

    }
}
