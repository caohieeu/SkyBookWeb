using SkyBookWeb.Application.Interfaces;
using SkyBookWeb.Core.Entities;
using SkyBookWeb.Core.Interfaces;

namespace SkyBookWeb.Application.Implements
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IGenericRepository<ApplicationUser> _applicationRepository;
        public ApplicationUserService(IGenericRepository<ApplicationUser> applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }
        public Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
