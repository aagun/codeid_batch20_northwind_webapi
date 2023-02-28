
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
    public class SupplierDto
    {

        //public int SupplierId{ get; set; }

        [Display(Name = "Supplier Name")]
        [Required]
        [StringLength(50, ErrorMessage = "Company name cannot be longer than 50")]
        public string? CompanyName { get; set; }


        [Required]
        public string? Address { get; set; }

      

    }
}
