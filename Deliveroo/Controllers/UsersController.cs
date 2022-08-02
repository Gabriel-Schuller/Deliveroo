using AutoMapper;
using Deliveroo.Data.Entities;
using Deliveroo.Helpers;
using Deliveroo.Models;
using Deliveroo.Service;
using Microsoft.AspNetCore.Authorization;
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
        private readonly JwtService _jwtService;

        public UsersController(IUserRepository repository, LinkGenerator linkGenerator, IMapper mapper,
            IBaseRepository baseRepository, JwtService jwtService)
        {
            _repository = repository;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
            _baseRepository = baseRepository;
            _jwtService = jwtService;
        }


        [HttpGet]
        [Authorize(Roles = "Administrator")]
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

        [HttpGet("/email/{email}")]
        [Authorize]
        public async Task<ActionResult<User>> GetUserWithEmail(string email)
        {
            try
            {
                var user = await _repository.GetUserByEmail(email);
                return user;
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

        [HttpGet("/all-admins")]
        [Authorize(Roles = "Administrator")]
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
        [Authorize]
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
        [AllowAnonymous]
        public async Task<ActionResult<UserModel>> Post([FromBody] UserModel model)
        {

            var existing = await _repository.GetUserByEmail(model.EmailAddress);

            if (existing != null) return BadRequest("User already in use");

            try
            {
                var user = _mapper.Map<User>(model);
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                var emptyAddress = _repository.GetEmptyUserAddress();

                _baseRepository.Add(emptyAddress);
                user.AddressID = emptyAddress.AddressID;

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

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login(UserModel model)
        {
            try
            {
                var user = await _repository.GetUserByEmail(model.EmailAddress);
                if (user == null) return BadRequest(new { message = "User doesn't exist" });

                if (!BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                {
                    return BadRequest(new { message = "User doesn't exist" });
                }

                var jwt = _jwtService.Generate(user);

                Response.Cookies.Append("jwt", jwt, new CookieOptions
                {
                    Secure = true,
                    HttpOnly = true,
                    IsEssential = true,
                    Expires = DateTime.Now.AddMonths(1),
                    SameSite = SameSiteMode.None

                });

                return Ok(jwt);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }


        [HttpPut("{id}")]
        [Authorize]
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

        [HttpPatch("/makeAdmin/{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> MakeAdmin(Guid id)
        {
            try
            {
                var oldUser = await _repository.GetById(id);
                if (oldUser == null)
                {
                    return NotFound("User with the specified id does not exist");
                }
                oldUser.Role = "Administrator";

                if (await _baseRepository.SaveChangesAsync())
                {
                    return Ok($"User {oldUser.UserName} is now an {oldUser.Role}");
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }

            return BadRequest();

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
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
