using Microservices.Order.Context;
using Microservices.Order.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Microservices.Order.Repository
{
    public class OrderRepository : IOrderRepository
    {
        //add dbcontext
        private readonly OrderDBContext _context;
        public OrderRepository(OrderDBContext context)
        {
            _context = context;
        }
        public List<Orders> GetOrders()
        {
            List<Orders> orders;
            try
            {
                orders = _context.Set<Orders>().ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return orders;
        }

        public Orders GetOrderById(int id)
        {
            //add logic to get order by id
            Orders order;
            try
            {
                order = _context.Find<Orders>(id);
            }
            catch (Exception)
            {
                throw;
            }
            return order;
        }

        public ResponseModel SaveOrder(Orders order)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                Orders orders = GetOrderById(order.Id);
                if (orders != null)
                {
                    orders.Name = order.Name;
                    orders.Price = order.Price;
                    orders.Quantity = order.Quantity;
                    orders.Description = order.Description;
                    _context.Update<Orders>(orders);
                    responseModel.Message = "Order updated successfully";
                }
                else
                {
                    _context.Add<Orders>(order);
                    responseModel.Message = "Order added successfully";
                }
                _context.SaveChanges();
                responseModel.Success = true;
            }
            catch (Exception ex)
            {
                responseModel.Message = "Error :" + ex.Message;
                responseModel.Success = false;
            }
            return responseModel;
        }

        //add delete method using response model
        public ResponseModel DeleteOrder(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                Orders order = GetOrderById(id);
                if (order != null)
                {
                    _context.Remove<Orders>(order);
                    _context.SaveChanges();
                    responseModel.Message = "Order deleted successfully";
                    responseModel.Success = true;
                }
                else
                {
                    responseModel.Message = "Order not found";
                    responseModel.Success = false;
                }
            }
            catch (Exception ex)
            {
                responseModel.Message = "Error :" + ex.Message;
                responseModel.Success = false;
            }
            return responseModel;
        }
    }
}
