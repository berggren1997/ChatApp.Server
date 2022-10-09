﻿using ChatApp.Entities.Models;
using ChatApp.Service.Contracts.Authentication;
using ChatApp.Shared.DataTransferObjects.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChatApp.Service.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IUserAccessor _userAccessor;

        private AppUser? _currentUser;

        public AuthService(UserManager<AppUser> userManager, IConfiguration configuration, 
            IUserAccessor userAccessor)
        {
            _userManager = userManager;
            _configuration = configuration;
            _userAccessor = userAccessor;
        }

        public async Task<IdentityResult> RegisterUser(UserRegistrationDto createUserDto)
        {
            var newUserEntity = new AppUser
            {
                UserName = createUserDto.Username,
                Email = createUserDto.Email
            };

            // TODO: Lägg till så att vi kastar ett eget fel ifall användare finns
            var result = await _userManager.CreateAsync(newUserEntity, createUserDto.Password);

            return result;
        }

        public async Task<bool> ValidateUser(UserAuthenticationDto userDto)
        {
            _currentUser = await _userManager.FindByNameAsync(userDto.Username);

            // TODO: Kasta custom exception
            if (_currentUser == null) throw new Exception();

            return _currentUser != null && await _userManager.CheckPasswordAsync(_currentUser, userDto.Password);
        }

        public async Task<List<UserDto>> SearchForUserByUsername(string username)
        {
            var users = await _userManager.Users
                .Where(x => x.UserName.Contains(username)).ToListAsync();

            if(users == null) 
                throw new Exception("No users with given search term was found.");

            var usersDto = users.Select(x => new UserDto
            {
                Id = x.Id,
                Username = x.UserName
            }).ToList();

            return usersDto;
        }

        public async Task<AppUser> GetCurrentUser()
        {
            var userId = _userAccessor.GetCurrentUserId();
            var user = await _userManager.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            return user;
        }

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return accessToken;
        }
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]);
            
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, _currentUser!.UserName),
                new Claim("userId", _currentUser.Id.ToString())
            };

            var roles = await _userManager.GetRolesAsync(_currentUser);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(10),
                signingCredentials: signingCredentials
                );

            return tokenOptions;
        }
    }
}