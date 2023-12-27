using MediatR;
using Microservices.Product.Models;
using Microservices.Product.Queries;
using Microservices.Product.Repositories;

namespace Microservices.Product.Handlers
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, List<Products>>
    {
        private readonly IProductRepository _productRepository;
        public GetProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        //add logic here
        public async Task<List<Products>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            return await _productRepository.GetAllProductsAsync();
        }
    }
}
