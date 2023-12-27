using MediatR;
using Microservices.Product.Models;
using Microservices.Product.Queries;
using Microservices.Product.Repositories;

namespace Microservices.Product.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Products>
    {
        private readonly IProductRepository _productRepository;
        public GetProductByIdHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Products> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
           //add logic here
            return await _productRepository.GetProductByIdAsync(request.Id);
        }
    }
}
