using Microservices.Customer.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microservices.Customer.Models
{
    [Table("Customer")]
    public class Customers : BaseEntity
    {
        public string? Name { get; set; }
       
        public DateTime DateOfBirth { get; set; }
      
        public string? Address { get; set; }
    }
}
