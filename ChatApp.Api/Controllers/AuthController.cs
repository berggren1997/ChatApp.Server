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

            var authResponse = await _service.AuthService.CreateToken(updateRefreshToken: true);

            SetRefreshToken(authResponse.RefreshToken);

            return Ok(new { accessToken = authResponse.AccessToken, username = authResponse.Username });
        }

        [HttpGet("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            
            if(refreshToken == null)
            {
                return Unauthorized("No valid token");
            }

            var authResponse = await _service.AuthService.RefreshToken(refreshToken);

            SetRefreshToken(authResponse.RefreshToken);
            
            return Ok(new { accessToken = authResponse.AccessToken, username = authResponse.Username });
        }

        [HttpGet("search/{username}")]
        public async Task<IActionResult> SearchUsers(string username)
        {
            var users = await _service.AuthService.SearchForUserByUsername(username);

            return users.Any() ? Ok(users) : NotFound("There were no users in database");
        }

        private void SetRefreshToken(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        /// <summary>
        /// Test method to check authenticaion in swagger
        /// </summary>
        /// <returns></returns>

        [HttpGet("current-user"), Authorize]
        public async Task<string> CurrentUser()
        {
            var user = User.Identity.Name;
            return user != null ? await Task.FromResult("Hello " + user) : "No user found";
        }
    }
}
