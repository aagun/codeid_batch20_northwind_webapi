using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Domain.Entities
{
    [Table("Users")]
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string? UserName { get; set; }


    }
}
