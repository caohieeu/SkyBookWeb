using Microsoft.AspNetCore.Mvc;
using SkyBookWeb.Application;
using SkyBookWeb.Core.Specifications;

namespace SkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Dashboard));
        }
    }
}
