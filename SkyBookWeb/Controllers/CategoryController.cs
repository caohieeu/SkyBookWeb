using Microsoft.AspNetCore.Mvc;
using SkyBookWeb.Infrastructure.Data;

namespace SkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        public CategoryController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var category = _dbContext.Categories.OrderBy(x => x.DisplayOrder).ToList();
            return View(category);
        }
    }
}
