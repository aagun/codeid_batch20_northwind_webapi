using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Domain.Entities
{
    public class SupplierJoinProduct
    {

        public int SupplierID { get; set; }
        public string? CompanyName { get; set; }

        public string? Address { get; set; }

        public int ProductID { get; set; }
        public string? ProductName { get; set; }

        public int CategoryID { get; set; }
        public string QuantityPerUnit { get; set; } = "1";
        public decimal UnitPrice { get; set; }
        public Int16 UnitsInStock { get; set; }
        public Int16 UnitsOnOrder { get; set; }
        public Int16 ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

    }
}
