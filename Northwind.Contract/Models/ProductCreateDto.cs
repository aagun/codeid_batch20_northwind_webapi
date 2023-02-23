
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Northwind.Contracts.Models
{
    public class ProductCreateDto
    {

       
        [Display(Name = "Product Name")]
        [Required]
        [StringLength(50, ErrorMessage = "Product name cannot be longer than 50")]
        public string? ProductName { get; set; }

        [Display(Name = "Supplier")]
        [Required]
        public int? SupplierId { get; set; }

        [Display(Name = "Category")]
        [Required]
        public int? CategoryId { get; set; }
        public string? QuantityPerUnit { get; set; }

        [Display(Name = "Units Price")]
        [Required]
        [Range(10, 9999999999999999.99)]
        public decimal? UnitPrice { get; set; }

        [Display(Name = "Units In Stock")]
        [Required]
        [Range(1, 1000)]
        public Int16 UnitsInStock { get; set; }

        [Display(Name = "Units In Stock")]
        [Required]
        [Range(1, 1000)]
        public Int16 UnitsOnOrder { get; set; }

        [Display(Name = "Units In Stock")]
        [Required]
        [Range(10, 1000)]
        public Int16 ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

    }
}
