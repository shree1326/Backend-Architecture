using MediatR;
using Microservices.Product.Commands;
using Microservices.Product.Models;
using Microservices.Product.Repositories;

namespace Microservices.Product.Handlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ResponseModel>
    {
        private readonly IProductRepository _productRepository;
        public UpdateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ResponseModel> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            ResponseModel responseModel = new();
            try
            {
                var response = await _productRepository.UpdateProductAsync(request.product);
                if (response == null)
                {
                    responseModel.Message = "Product not found";
                    responseModel.Success = false;
                }
                else
                {
                    responseModel.Message = "Product updated successfully";
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
