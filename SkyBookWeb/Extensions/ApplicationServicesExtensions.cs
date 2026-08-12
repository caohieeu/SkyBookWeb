using Microsoft.Extensions.DependencyInjection;
using SkyBookWeb.Application;
using SkyBookWeb.Application.Implements;
using SkyBookWeb.Application.Interfaces;
using SkyBookWeb.Core.Interfaces;
using SkyBookWeb.Infrastructure.Data;
using SkyBookWeb.Infrastructure.Repositories;

namespace SkyBookWeb.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddTransient<MappingProfiles.ProductMapping>();

            //Auto mapper
            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(typeof(MappingProfiles).Assembly);
            });
        }
    }
}
