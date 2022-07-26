using Deliveroo.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deliveroo.Service.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly DataContext _context;

        public AddressRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Address>> GetAddressesByCity(string city)
        {
            return await _context.Address.Where(a => a.City == city).ToListAsync();
        }

        public async Task<Address> GetAddressById(Guid addressId)
        {
            return await _context.Address.FindAsync(addressId);
        }

        public async Task<List<Address>> GetAddressesByPostalCode(string code)
        {
            return await _context.Address.Where(a => a.PostalCode == code).ToListAsync();
        }

        public async Task<List<Address>> GetAllAddresses()
        {
            return await _context.Address.ToListAsync();
        }

        public async Task<List<Address>> GetAllUserAddresses(Guid userId)
        {
            var addresses = await _context.Orders.Include(o => o.Address).Where(o => o.UserID == userId).Select(o => o.Address).ToListAsync();
            var user = await _context.Users.FindAsync(userId);
            addresses.Add(await GetAddressById(user.AddressID));
            return addresses;
        }

        public async Task<Address> GetOrderAddress(Guid orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            return order.Address;
        }
    }
}
