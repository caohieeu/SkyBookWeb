using System.Runtime;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SkyBookWeb.Application;
using SkyBookWeb.Application.Implements;
using SkyBookWeb.Application.Interfaces;
using SkyBookWeb.Core.Entities;
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
        
        public static void AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<ApplicationDBContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/identity/account/login";
                options.LogoutPath = "/identity/account/lougout";
                options.AccessDeniedPath = "/identity/account/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
            });
        }
    }
}
