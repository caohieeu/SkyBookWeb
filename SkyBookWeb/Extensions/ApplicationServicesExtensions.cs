using SkyBookWeb.Core.Interfaces;
using SkyBookWeb.Core.Specifications;
using SkyBookWeb.Infrastructure.Data;
using SkyBookWeb.Infrastructure.Repositories;
using System.ComponentModel;

namespace SkyBookWeb.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
