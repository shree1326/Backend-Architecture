using Microservices.Order.Models;

namespace Microservices.Order.Repository
{
    public interface IOrderRepository
    {
        //add list of methods here
        List<Orders> GetOrders();
        Orders GetOrderById(int id);
        ResponseModel SaveOrder(Orders order);
        ResponseModel DeleteOrder(int id);
    }
}
