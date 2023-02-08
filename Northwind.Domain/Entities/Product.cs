using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Products")]
    public class Product
    {

        [Key]
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        [ForeignKey("Suppliers")]
        public int SupplierID { get; set; }
        [ForeignKey("Categories")]
        public int CategoryID { get; set; }
        public string QuantityPerUnit { get; set; } = "1";
        public decimal UnitPrice { get; set; }
        public Int16 UnitsInStock { get; set; }
        public Int16 UnitsOnOrder { get; set; }
        public Int16 ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

    }
}
