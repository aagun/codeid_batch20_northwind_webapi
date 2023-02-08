using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Contract.Models
{
    public class RegionDto
    {
        [Required(ErrorMessage ="RegonId is required")]
        public int RegionId { get; set; }

        [Required]
        [MinLength(5, ErrorMessage ="RegionDescription must larger than 5")]
        [MaxLength(50, ErrorMessage = "RegionDescription cannot be longer than 50")]
        public string? RegionDescription { get; set; }
    }
}
