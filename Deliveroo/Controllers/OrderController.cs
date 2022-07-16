using AutoMapper;
using Deliveroo.Models;
using Deliveroo.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deliveroo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;
        private readonly IBaseRepository _baseRepository;

        public OrderController(, LinkGenerator linkGenerator, IMapper mapper, IBaseRepository baseRepository)
        {
            _linkGenerator = linkGenerator;
            _mapper = mapper;
            _baseRepository = baseRepository;
        }

        //[HttpGet]
        //public async Task<List<AddressModel>> Get()
        //{
        //    try
        //    {
        //        return await 
        //    }
        //    catch (System.Exception)
        //    {

        //        throw;
        //    }
        //}

    }
}
