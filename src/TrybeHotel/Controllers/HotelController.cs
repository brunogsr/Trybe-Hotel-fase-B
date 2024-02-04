using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;
using TrybeHotel.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("hotel")]
    public class HotelController : Controller
    {
        private readonly IHotelRepository _repository;

        public HotelController(IHotelRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetHotels()
        {
            return Ok(_repository.GetHotels());
        }

        [HttpPost]
        public IActionResult PostHotel([FromBody] Hotel hotel)
        {
            // only admin
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            if (role != "admin")
            {
                return Unauthorized();
            }
            return Created("hotel", _repository.AddHotel(hotel));
        }


    }
}