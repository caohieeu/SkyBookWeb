using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SkyBookWeb.Application.Interfaces;

namespace SkyBookWeb.Application.Implements
{
    public class UserIdentityService : IUserIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string? GetUserId()
        {
            return _httpContextAccessor.HttpContext?
                .User?
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
