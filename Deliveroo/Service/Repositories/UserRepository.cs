using AutoMapper;
using Deliveroo.Data.Entities;
using Deliveroo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deliveroo.Service.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Address GetEmptyUserAddress()
        {
            var address= _mapper.Map<Address>(new OrderModel());
            address.AddressID = Guid.NewGuid();
            return address;
        }

        public async Task<List<User>> GetAllAdministratorsAsync()
        {
            return await _context.Users.Where(u => u.Role == "Administrator").ToListAsync();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetById(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.EmailAddress == email);
        }

    }
}
