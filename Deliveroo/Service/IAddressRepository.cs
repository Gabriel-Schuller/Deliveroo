using Deliveroo.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deliveroo.Service
{
    public interface IAddressRepository
    {

        Task<List<Address>> GetAllAddresses();
        Task<List<Address>> GetAllUserAddresses(Guid userId);

        Task<List<Address>> GetAddressesByCity(string city);
        Task<List<Address>> GetAddressesByPostalCode(string code);
        Task<List<Address>> GetOrderAddress(Guid orderId);
    
    }
}
