using ChatApp.Service.Contracts;
using ChatApp.Shared.DataTransferObjects.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
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

            var token = await _service.AuthService.CreateToken(updateRefreshToken: true);

            SetRefreshToken(token.RefreshToken);

            return Ok(new { token = token.AccessToken });
        }

        [HttpPost("refresh"), Authorize]
        public async Task<IActionResult> RefreshToken([FromBody] string accessToken)
        {
            var refreshToken = Request.Cookies["refreshToken"];
            
            if(refreshToken == null)
            {
                return Unauthorized("No valid token");
            }
            var tokens = await _service.AuthService.RefreshToken(accessToken, refreshToken);

            SetRefreshToken(tokens.RefreshToken);

            return Ok(new { accessToken = tokens.AccessToken });
        }

        [HttpGet("search/{username}")]
        public async Task<IActionResult> SearchUsers(string username)
        {
            var users = await _service.AuthService.SearchForUserByUsername(username);

            return users != null ? Ok(users) : NotFound("There were no users in database");
        }

        private void SetRefreshToken(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        /// <summary>
        /// Test method to check authenticaion in swagger
        /// </summary>
        /// <returns></returns>

        [HttpGet("current-user"), Authorize]
        public string CurrentUser()
        {
            var user = User.Identity.Name;
            return user != null ? $"Hello {user}" : "No user found";
        }
    }
}
