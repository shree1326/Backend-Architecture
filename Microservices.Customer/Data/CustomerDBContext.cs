using Microservices.Customer.Models;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Customer.Data
{
    public class CustomerDBContext : DbContext
    {
        public CustomerDBContext(DbContextOptions options) : base(options) { }
        public DbSet<Customers> Customers { get; set; }
       
    }
}
