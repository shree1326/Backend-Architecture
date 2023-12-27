using MediatR;
using Microservices.Product.Commands;
using Microservices.Product.Models;
using Microservices.Product.Repositories;

namespace Microservices.Product.Handlers
{
    public class AddProductHandler : IRequestHandler<AddProductCommand, ResponseModel> //Products
    {
        private readonly IProductRepository _productRepository;
        public AddProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ResponseModel> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
           ResponseModel responseModel  = new();
            try
            {
                var response = await _productRepository.AddProductAsync(request.product);
                if (response == null)
                {
                    responseModel.Message = "Product not found";
                    responseModel.Success = false;
                }
                else
                {
                    responseModel.Message = "Product added successfully";
                    responseModel.Success = true;
                }
                return responseModel;
            }
            catch (Exception ex)
            {
                responseModel.Success = false;
                responseModel.Message = ex.Message;
                return responseModel;
            }
        }
    }
}
