using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SkyBookWeb.Application.Interfaces;
using SkyBookWeb.Core.Entities;
using SkyBookWeb.Core.Interfaces;

namespace SkyBookWeb.Application.Implements
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IGenericRepository<ApplicationUser, string> _applicationRepository;
        public ApplicationUserService(IGenericRepository<ApplicationUser, string> applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }
        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _applicationRepository.GetIdAsync(userId);
        }
    }
}
