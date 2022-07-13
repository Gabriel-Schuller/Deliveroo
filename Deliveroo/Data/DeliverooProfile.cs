using AutoMapper;
using Deliveroo.Data.Entities;
using Deliveroo.Models;

namespace Deliveroo.Data
{
    public class DeliverooProfile : Profile
    {
        public DeliverooProfile()
        {
            this.CreateMap<Address, AddressModel>().ReverseMap();
            this.CreateMap<User, UserModel>().ReverseMap();
            this.CreateMap<Order, OrderModel>().ReverseMap();
        }
    }
}
