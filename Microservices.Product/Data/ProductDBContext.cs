using Microservices.Product.Models;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Product.Data
{
    public class ProductDBContext : DbContext
    {
        // add dbsets here
        public DbSet<Products> Products { get; set; }
        public ProductDBContext(DbContextOptions<ProductDBContext> options) : base(options)
        {
        }
    }
}
