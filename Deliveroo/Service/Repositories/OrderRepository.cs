﻿using Deliveroo.Data.Entities;
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

        public const int WeightDivider = 30;
        public const int Price = 20;

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
            return await _context.Orders.Include(o => o.Address).Where(o => o.OrderID == orderId).FirstOrDefaultAsync();
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
            int calculatedPrice = (order.TotalWeight / WeightDivider) * Price;
            if (order.TotalWeight % WeightDivider != 0)
            {
                calculatedPrice += Price;
            }
            return calculatedPrice;
        }
    }
}
