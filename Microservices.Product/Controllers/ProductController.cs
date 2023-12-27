using MediatR;
using Microservices.Product.Commands;
using Microservices.Product.Models;
using Microservices.Product.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Microservices.Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    { 
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        //add async logic to get all products list
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<List<Products>> GetProductsAsync()
        {
            //add logic to get all products list from query
            var products =  await _mediator.Send(new GetProductsQuery());
            return products;
        }
        //add async logic to get product by id from query
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<Products> GetProductByIdAsync(int id)
        {
            //add logic to get product by id from query
            var product = await _mediator.Send(new GetProductByIdQuery() { Id = id});
            return product;
        }
        //add async logic to add product from command
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ResponseModel> AddProductAsync(Products product)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                //add logic to add product from command
                var result = await _mediator.Send(new AddProductCommand(product));
                response.Message = "Product added successfully";
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Can not add Product";
                response.Success = false;
                return response;
            }
        }
        //add async logic to update product from command
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ResponseModel> UpdateProductAsync(Products product)
        {
            //add logic to update product from command
            var result = await _mediator.Send(new UpdateProductCommand(product));
            return result;
        }
        //add async logic to delete product from command
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ResponseModel> DeleteProductAsync(int id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                //add logic to delete product from command
                var result = await _mediator.Send(new DeleteProductCommand(id));
                response.Message = "Product deleted successfully";
                response.Success = true;
                return response;
            }
            catch (Exception)
            {
                response.Message = "Can not delete Product";
                response.Success = false;
                return response;
            }
        }
       
    }
}
