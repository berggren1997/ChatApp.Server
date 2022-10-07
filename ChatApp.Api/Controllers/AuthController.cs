using ChatApp.Service.Contracts;
using ChatApp.Shared.DataTransferObjects.User;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IServiceManager _service;

        public AuthController(IServiceManager service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationDto newUser)
        {
            var response = await _service.AuthService.RegisterUser(newUser);

            if (!response.Succeeded) return BadRequest("Bad request from client");

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserAuthenticationDto user)
        {
            var currentUser = await _service.AuthService.ValidateUser(user);

            if (!currentUser) return BadRequest("Bad credentials");
            
            return Ok(new { token = await _service.AuthService.CreateToken() });
        }
    }
}
