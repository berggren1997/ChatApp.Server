using ChatApp.Service.Contracts.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ChatApp.Service.Authentication
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _context;

        public UserAccessor(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string? GetCurrentUserName()
        {
            var username = _context?.HttpContext?.User?.Claims?
                .FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            return username;
        }

        /// <summary>
        /// Reads the claims from incoming request, and parses the id to an integer
        /// </summary>
        /// <returns>userid or -1</returns>
        public int GetCurrentUserId()
        {
            string? userIdString = _context?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == "userId").Value ?? null;

            if (userIdString == null) return 0;
            return int.TryParse(userIdString, out var id) ? id : 0;
        }
    }
}
