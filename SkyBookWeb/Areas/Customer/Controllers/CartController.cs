using Microsoft.AspNetCore.Mvc;
using SkyBookWeb.Application;
using SkyBookWeb.Application.Interfaces;
using SkyBookWeb.Core.Specifications;

namespace SkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IShoppingCartService _shoppingCartService;
        public CartController(ILogger<HomeController> logger, 
            IShoppingCartService shoppingCartService)
        {
            _logger = logger;
            _shoppingCartService = shoppingCartService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
