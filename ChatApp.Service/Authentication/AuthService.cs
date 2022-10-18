using ChatApp.Entities.Exceptions.NotFoundRequests;
using ChatApp.Entities.Models;
using ChatApp.Service.Contracts.Authentication;
using ChatApp.Shared.DataTransferObjects.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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

            if (_currentUser == null)
                throw new UserNotFoundException();

            return _currentUser != null && await _userManager.CheckPasswordAsync(_currentUser, userDto.Password);
        }

        public async Task<List<UserDto>> SearchForUserByUsername(string username)
        {
            var user = await _userManager.Users
                .Where(x => x.UserName.Contains(username)).ToListAsync();

            if (user == null)
                throw new UserNotFoundException();

            var usersDto = user.Select(x => new UserDto
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

        public async Task<JwtTokenDto> CreateToken(bool populateRefreshToken)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            var refreshToken = GenerateRefreshToken();

            _currentUser!.RefreshToken = refreshToken;

            if (populateRefreshToken)
                _currentUser.RefreshTokenExpiryDate = DateTime.Now.AddDays(7);

            await _userManager.UpdateAsync(_currentUser!);

            return new JwtTokenDto { AccessToken = accessToken, RefreshToken = refreshToken };
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
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: signingCredentials
                );

            return tokenOptions;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public async Task<JwtTokenDto> RefreshToken(/*string accessToken, */string refreshToken)
        {
            //var principals = GetClaimsPrincipalFromExpiredToken(accessToken);
            _currentUser = await _userManager.Users.Where(x => x.RefreshToken == refreshToken)
                .FirstOrDefaultAsync();

            //var user = await _userManager.FindByNameAsync(principals.Identity!.Name);

            if (_currentUser == null || _currentUser.RefreshToken != refreshToken ||
                _currentUser.RefreshTokenExpiryDate <= DateTime.Now)
            {
                //TODO: Kasta ett custom fel här
                throw new Exception($"Invalid token passed in {nameof(RefreshToken)} method.");
            }

            //_currentUser = user;

            return await CreateToken(populateRefreshToken: false);
        }

        private ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string accessToken)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]))
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out var securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token.");
            }
            return principal;
        }
    }
}
