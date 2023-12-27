using Microservices.Order.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Microservices.Order.Context
{
    public class OrderDBContext : DbContext
    {
        //add dbcontext
        public OrderDBContext(DbContextOptions<OrderDBContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        //add dbcontext
        public DbSet<Orders> Orders { get; set; } 

    }
}
