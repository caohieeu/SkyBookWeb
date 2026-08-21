using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using SkyBookWeb.Application.Implements;
using SkyBookWeb.Application.Interfaces;
using SkyBookWeb.Core.Entities;
using SkyBookWeb.Extensions;
using SkyBookWeb.Infrastructure.Data;
using SkyBookWeb.Infrastructure.SeedData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection"));
});

builder.Services.AddIdentityServices();

//File Service
builder.Services.AddScoped<IFileService>(provider =>
{
    return new FileService(builder.Environment.WebRootPath);
});

//Register services
builder.Services.AddApplicationServices();

var app = builder.Build();

// Db Migration
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetService<ILoggerFactory>();
    var roleManager = services.GetService<RoleManager<IdentityRole>>(); 
    
    string[] roleNames = { SkyBookWeb.Utilty.Constant.RoleEmployee,
        SkyBookWeb.Utilty.Constant.RoleCustomer, SkyBookWeb.Utilty.Constant.RoleAdmin };

    try
    {
        var context = services.GetService<ApplicationDBContext>();
        if(logger != null && context != null)
        {
            await DataContextSeed.SeedAsync(context, logger);
        }

        if (roleManager != null)
        {
            foreach (var role in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

    } catch(Exception ex)
    {
        var loggerDbContext = logger.CreateLogger<ApplicationDBContext>();
        loggerDbContext.LogError(ex, "An error occured during application startup");
    }
}

using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthorization();

app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}",
    defaults: new { area = "Customer" });

app.Run();
