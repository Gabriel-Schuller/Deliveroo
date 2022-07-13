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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Deliveroo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;
        private readonly IBaseRepository _baseRepository;

        public UsersController(IUserRepository repository, LinkGenerator linkGenerator, IMapper mapper, IBaseRepository baseRepository)
        {
            _repository = repository;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
            _baseRepository = baseRepository;
        }


        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            try
            {
                var users = await _repository.GetAllUsersAsync();

                return users;
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

        [HttpGet("/all-admins")]
        public async Task<ActionResult<List<User>>> GetAllAdmins()
        {
            try
            {
                var users = await _repository.GetAllAdministratorsAsync();

                return users;
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }


        [HttpGet("{userID}")]
        public async Task<ActionResult<UserModel>> Get(Guid userID, bool makeAdmin = false)
        {
            try
            {
                var result = await _repository.GetById(userID);
                if (result == null)
                {
                    return this.NotFound();
                }
                if (makeAdmin)
                {
                    result.Role = "Administrator";
                    if (await _baseRepository.SaveChangesAsync())
                    {
                        return _mapper.Map<UserModel>(result);
                    }

                }
                return _mapper.Map<UserModel>(result);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");

            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserModel>> Post([FromBody] UserModel model)
        {

            var existing = await _repository.GetUserByEmail(model.EmailAddress);

            if (existing != null) return BadRequest("User already in use");

            try
            {
                var user = _mapper.Map<User>(model);
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                _baseRepository.Add(user);

                if (await _baseRepository.SaveChangesAsync())
                {
                    var location = _linkGenerator.GetPathByAction("Get", "Users", new { userID = user.UserID });

                    if (string.IsNullOrWhiteSpace(location))
                    {
                        return BadRequest("Could not use current id");
                    }

                    return Created(location, _mapper.Map<UserModel>(user));

                }

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");

            }

            return BadRequest();
        }

        //TO DO IMPLEMENT LOGIN AND LOGOUT FUNCTIONALITY

        [HttpPut("{id}")]
        public async Task<ActionResult<UserModel>> Put(Guid id, UserModel model)
        {
            try
            {
                var oldUser = await _repository.GetById(id);
                if (oldUser == null)
                {
                    return NotFound("User with the specified id does not exist");
                }
                _mapper.Map(model, oldUser);

                if (await _baseRepository.SaveChangesAsync())
                {
                    return _mapper.Map<UserModel>(oldUser);
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var oldUser = await _repository.GetById(id);
                if (oldUser == null)
                {
                    return NotFound("There is no user with the specified id");
                }

                _baseRepository.Delete(oldUser);

                if (await _baseRepository.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
            return BadRequest("Failed to delete the camp!");
        }
    }
}
