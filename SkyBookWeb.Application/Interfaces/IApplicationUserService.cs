using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkyBookWeb.Core.Entities;

namespace SkyBookWeb.Application.Interfaces
{
    public interface IApplicationUserService
    {
        Task<ApplicationUser> GetUserByIdAsync(string userId);
    }
}
