using MediatR;
using Microservices.Product.Models;

namespace Microservices.Product.Queries
{
    public class GetProductsQuery : IRequest<List<Products>> { }
}
