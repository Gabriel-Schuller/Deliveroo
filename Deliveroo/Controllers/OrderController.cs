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
        private readonly IOrderRepository _repository;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;
        private readonly IBaseRepository _baseRepository;
        private readonly IUserRepository _userRepository;

        public OrderController(IOrderRepository repository, LinkGenerator linkGenerator, IMapper mapper, IBaseRepository baseRepository, IUserRepository userRepository)
        {
            _repository = repository;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
            _baseRepository = baseRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> Get()
        {
            try
            {
                return await _repository.GetAllOrders();
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }


        [HttpGet("{orderId}")]
        public async Task<ActionResult<Order>> GetSpecificOrder(Guid orderId)
        {
            try
            {
                return await _repository.GetOrderById(orderId);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }


        [HttpGet("/user/{userId}")]
        public async Task<ActionResult<List<Order>>> GetUserOrder(Guid userId)
        {
            try
            {
                return await _repository.GetUserOrders(userId);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }


        [HttpGet("/fromDate/{fromDate}")]
        public async Task<ActionResult<List<Order>>> GetOrdersFromSpecificDate(DateTime fromDate)
        {
            try
            {
                return await _repository.GetAllOrdersFromSpecificDate(fromDate);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }
        

        [HttpGet("/onDate/{theDate}")]
        public async Task<ActionResult<List<Order>>> GetOrdersOnSpecificDate(DateTime theDate)
        {
            try
            {
                return await _repository.GetAllOrdersOnSpecificDate(theDate);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

        [HttpPost("{userEmail}")]
        public async Task<ActionResult<OrderModel>> AddOrder(string userEmail, OrderModel model)
        {
            try
            {
                var order = _mapper.Map<Order>(model);
                var user =await _userRepository.GetUserByEmail(userEmail);
                order.UserID = user.UserID;
                order.AproxCost = _repository.CalculatePrice(order);
                _baseRepository.Add(order);
                if (await _baseRepository.SaveChangesAsync())
                {
                    var location = _linkGenerator.GetPathByAction("GetSpecificOrder", "Order", new { orderId = order.OrderID });
                    if (string.IsNullOrWhiteSpace(location))
                    {
                        return BadRequest("Could not use current id");
                    }
                    return Created(location, _mapper.Map<OrderModel>(order));
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
            return BadRequest();
        }

        [HttpPut("{orderId}")]
        public async Task<ActionResult<OrderModel>> UpdateAddress(Guid orderId, OrderModel model)
        {
            try
            {
                var oldOrder = await _repository.GetOrderById(orderId);
                if (oldOrder == null)
                {
                    return NotFound("Order with the specified id does not exist");
                }
                _mapper.Map(model, oldOrder);
                if (await _baseRepository.SaveChangesAsync())
                {
                    return _mapper.Map<OrderModel>(oldOrder);
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
            return BadRequest();
        }


        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteAddress(Guid orderId)
        {
            try
            {
                var oldOrder = await _repository.GetOrderById(orderId);
                if (oldOrder == null)
                {
                    return NotFound("There is no order with the specified id");
                }
                _baseRepository.Delete(oldOrder);
                if (await _baseRepository.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
            return BadRequest("Failed to delete the order!");
        }

    }
}
