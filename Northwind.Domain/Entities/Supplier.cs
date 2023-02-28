using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Domain.Entities
{
    [Table("Suppliers")]
    public class Supplier
    {
        [Key]
        public int SupplierID{ get; set; }

        public string? CompanyName{ get; set; } 

        public string? Address { get; set; }


    }
}
