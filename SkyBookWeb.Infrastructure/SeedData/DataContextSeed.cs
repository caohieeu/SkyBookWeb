using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SkyBookWeb.Core.Entities;
using SkyBookWeb.Infrastructure.Data;

namespace SkyBookWeb.Infrastructure.SeedData
{
    public class DataContextSeed
    {
        public static async Task SeedAsync(ApplicationDBContext dataContext, ILoggerFactory loggerFactory)
        {
            try
            {
                if(!dataContext.Categories.Any())
                {
                    var dataFileCategories = File.ReadAllText("../SkyBookWeb.Infrastructure/SeedData/category.json");
                    var categories = JsonSerializer.Deserialize<List<Category>>(dataFileCategories);
                    if(categories != null && categories.Any())
                    {
                        foreach (var cate in categories)
                        {
                            await dataContext.AddAsync(cate);
                        }
                        await dataContext.SaveChangesAsync();
                    }
                }
            }
            catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<DataContextSeed>();
                logger.LogError(ex, "Something went wrong with seed data");
            }
        }
    }
}
