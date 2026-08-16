using Microsoft.AspNetCore.Mvc;
using SkyBookWeb.Application;
using SkyBookWeb.Core.Specifications;

namespace SkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var specParam = new ProductSpecPrams()
            {
                PageSize = 5
            };
            var products = await _productService.GetAllWithSpecification(specParam);
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        public IActionResult TestEndPoint()
        {
            return Json(new { data = "asd" });
        }
    }
}
