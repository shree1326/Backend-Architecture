using MediatR;
using Microservices.Product.Commands;
using Microservices.Product.Models;
using Microservices.Product.Repositories;

namespace Microservices.Product.Handlers
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, ResponseModel>
    {
        private readonly IProductRepository _productRepository;
        public DeleteProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ResponseModel> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            ResponseModel responseModel = new();
            try
            {
                var response = await _productRepository.DeleteProductAsync(request.Id);
                if(response == null)
                {
                    responseModel.Message = "Product not found";
                    responseModel.Success = false; 
                }
                else
                {
                    responseModel.Message = "Product deleted successfully";
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
