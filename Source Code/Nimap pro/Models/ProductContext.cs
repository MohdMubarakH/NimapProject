using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Nimap_pro.Models
{
    public class ProductContext :DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options):base(options)
        {

        }

        public DbSet<Product> tblProduct { get; set; }
        public DbSet<Category> tblCategory { get; set; }
    }
}
