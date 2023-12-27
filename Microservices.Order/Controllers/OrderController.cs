using Microservices.Order.Context;
using Microservices.Order.Models;
using Microservices.Order.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Microservices.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        //inject IOrderRepository
        private readonly IOrderRepository _orderrepo;
        //inject IOrderRepository
        public OrderController(IOrderRepository orderrepo)
        {
            _orderrepo = orderrepo;
        }
        //get all orders and handle try catch
        [HttpGet]
        [Authorize]
        public IActionResult GetOrders()
        {
            try
            {
                var orders = _orderrepo.GetOrders();
                if(orders == null)
                {
                    return NotFound();
                }
                return Ok(orders);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        //get order by id and handle try catch
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetOrderById(int id)
        {
            try
            {
                var order = _orderrepo.GetOrderById(id);
                if(order == null)
                {
                    return NotFound();
                }
                return Ok(order);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        //add order and handle try catch
        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public IActionResult SaveOrder(Orders order)
        {
            try
            {
                var result = _orderrepo.SaveOrder(order);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        //delete order by finding the Id to delete and handle try catch
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                var result = _orderrepo.DeleteOrder(id);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


    }
}
