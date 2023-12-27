using MediatR;
using Microservices.Product.Models;

namespace Microservices.Product.Queries
{
    public class GetProductByIdQuery : IRequest<Products>
    {
        //add properties here
        public int Id { get; set; }
    }
}
