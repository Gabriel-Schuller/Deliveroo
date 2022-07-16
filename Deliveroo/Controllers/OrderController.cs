using AutoMapper;
using Deliveroo.Data.Entities;
using Deliveroo.Models;
using Deliveroo.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deliveroo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IAddressRepository _repository;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;
        private readonly IBaseRepository _baseRepository;

        public OrderController(IAddressRepository repository, LinkGenerator linkGenerator, IMapper mapper, IBaseRepository baseRepository)
        {
            _repository = repository;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
            _baseRepository = baseRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Address>>> Get()
        {
            try
            {
                return await _repository.GetAllAddresses();

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

        [HttpGet("{userID}")]
        public async Task<ActionResult<List<Address>>> GetAddressByUser(Guid userID)
        {
            try
            {
                return await _repository.GetAllUserAddresses(userID);

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

        [HttpGet("/city/{city}")]
        public async Task<ActionResult<List<Address>>> GetAddressByCity(string city)
        {
            try
            {
                return await _repository.GetAddressesByCity(city);

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

        [HttpGet("/postalcode/{code}")]
        public async Task<ActionResult<List<Address>>> GetAddressByCode(string code)
        {
            try
            {
                return await _repository.GetAddressesByPostalCode(code);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

    }
}
