using MediatR;
using Microservices.Product.Models;

namespace Microservices.Product.Commands
{
    public record AddProductCommand(Products product) : IRequest<ResponseModel>;
}
