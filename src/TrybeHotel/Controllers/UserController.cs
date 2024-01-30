using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Repository;
using TrybeHotel.Dto;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var user = HttpContext.User.Identity as ClaimsIdentity;
            var userType = user?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (userType == null)
            {
                return Unauthorized();
            }
            var allUsers = _repository.GetUsers();
            return Ok(allUsers);
        }

        [HttpPost]
        public IActionResult Add([FromBody] UserDtoInsert user)
        {
            var email = _repository.GetUserByEmail(user.Email);

            if (email != null)
            {
                return Conflict(new { message = "User email already exists" });
            }

            var addedUser = _repository.Add(user);

            return Created("", addedUser);
        }
    }
}
