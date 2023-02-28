using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Domain.Entities
{
    public class SupplierNestedProduct
    {
        public int SupplierID { get; set; }
        public string? CompanyName{ get; set; }

        public string? Address { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
