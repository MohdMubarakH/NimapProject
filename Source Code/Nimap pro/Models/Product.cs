using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nimap_pro.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required(ErrorMessage ="Required")]
        public string ProductName { get; set; }

        public int CategoryID { get; set; }
       [NotMapped]
       
        public string CategoryName { get; set; }
    }
}
