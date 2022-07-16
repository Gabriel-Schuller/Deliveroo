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
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _repository;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;
        private readonly IBaseRepository _baseRepository;

        public AddressController(IAddressRepository repository, LinkGenerator linkGenerator, IMapper mapper, IBaseRepository baseRepository)
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

        [HttpGet("/order/{orderId}")]
        public async Task<ActionResult<Address>> GetAddressByOrderID(Guid orderId)
        {
            try
            {
                return await _repository.GetOrderAddress(orderId);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

        [HttpGet("{addressId}")]
        public async Task<ActionResult<AddressModel>> GetAddressById(Guid id)
        {
            try
            {

                var address = await _repository.GetOrderAddress(id);
                return _mapper.Map<AddressModel>(address);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AddressModel>> AddAddress([FromBody] AddressModel model)
        {
            try
            {
                var address = _mapper.Map<Address>(model);
                _baseRepository.Add(address);
                if (await _baseRepository.SaveChangesAsync())
                {
                    var location = _linkGenerator.GetPathByAction("GetAddressById", "Address", new { addressId = address.AddressID });
                    if (string.IsNullOrWhiteSpace(location))
                    {
                        return BadRequest("Could not use current id");
                    }
                    return Created(location, _mapper.Map<AddressModel>(address));
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
            return BadRequest();
        }

        [HttpPut("{addressId}")]
        public async Task<ActionResult<AddressModel>> UpdateAddress(Guid addressId, AddressModel model)
        {
            try
            {
                var oldAddress = await _repository.GetAddressById(addressId);
                if (oldAddress == null)
                {
                    return NotFound("Address with the specified id does not exist");
                }
                _mapper.Map(model, oldAddress);
                if (await _baseRepository.SaveChangesAsync())
                {
                    return _mapper.Map<AddressModel>(oldAddress);
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
            return BadRequest();
        }

        [HttpDelete("{addressId}")]
        public async Task<IActionResult> DeleteAddress(Guid addressId)
        {
            try
            {
                var oldAddress = await _repository.GetAddressById(addressId);
                if (oldAddress == null)
                {
                    return NotFound("There is no address with the specified id");
                }
                _baseRepository.Delete(oldAddress);
                if (await _baseRepository.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
            return BadRequest("Failed to delete the address!");
        }


    }
}
