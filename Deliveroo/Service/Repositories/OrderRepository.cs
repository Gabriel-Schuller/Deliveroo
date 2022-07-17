using Deliveroo.Data.Entities;
using Deliveroo.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deliveroo.Service.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _context.Orders.ToListAsync();
        }


        public async Task<Order> GetOrderById(Guid orderId)
        {
            return await _context.Orders.FindAsync(orderId);
        }

        public async Task<List<Order>> GetUserOrders(Guid id)
        {
           

            return await _context.Orders.Where(o => o.UserID == id).ToListAsync();
        }


        public async Task<List<Order>> GetAllOrdersFromSpecificDate(DateTime date)
        {
            return await _context.Orders.Where(o => DateTime.Compare(o.OrderDate, date) >= 0).ToListAsync();
        }

        public async Task<List<Order>> GetAllOrdersOnSpecificDate(DateTime date)
        {
            return await _context.Orders.Where(o => DateTime.Compare(date.Date, o.OrderDate.Date) == 0).ToListAsync();
        }

        public int CalculatePrice(Order order)
        {
            int calculatedPrice = (order.TotalWeight / Util.WeightDivider) * Util.Price;
            if (order.TotalWeight % Util.WeightDivider != 0)
            {
                calculatedPrice += Util.Price;
            }
            return 1;
        }
    }
}
