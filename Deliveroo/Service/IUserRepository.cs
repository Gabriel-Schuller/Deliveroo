using Deliveroo.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deliveroo.Service
{
    public interface IUserRepository
    {
        Task<User> GetById(Guid id);


        Task<List<User>> GetAllUsersAsync();
        Task<List<User>> GetAllAdministratorsAsync();

        Task<User> GetUserByEmail(string email);

    }
}
