using MediatR;
using Microservices.Product.Models;

namespace Microservices.Product.Commands
{
    public record UpdateProductCommand(Products product) : IRequest<ResponseModel>;
}
