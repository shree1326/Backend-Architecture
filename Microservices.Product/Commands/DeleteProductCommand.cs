using MediatR;
using Microservices.Product.Models;

namespace Microservices.Product.Commands
{
    public class DeleteProductCommand : IRequest<ResponseModel>
    {
        //add properties here
        public int Id { get; set; }
        public DeleteProductCommand(int id)
        {
            Id = id;
        }
    }
}
