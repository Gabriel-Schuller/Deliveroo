using Deliveroo.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deliveroo.Service
{
    public interface IOrderRepository
    {
        //Get Orders by date to be implemented
        Task<List<Order>> GetAllOrders();
        
        Task<Order> GetOrderById(Guid orderId);
        Task<List<Order>> GetUserOrders(Guid id);
    }
}
