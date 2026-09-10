using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkyBookWeb.Application;
using SkyBookWeb.Application.Interfaces;
using SkyBookWeb.Core.Specifications;
using SkyBookWeb.Models.ViewModels;

namespace SkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IUserIdentityService _userIdentityService;
        public CartController(ILogger<HomeController> logger, 
            IShoppingCartService shoppingCartService,
            IUserIdentityService userIdentityService)
        {
            _logger = logger;
            _shoppingCartService = shoppingCartService;
            _userIdentityService = userIdentityService;
        }

        public async Task<IActionResult> Index()
        {
            var cartItems = await _shoppingCartService.GetUserCartItemsAsync(_userIdentityService.GetUserId());

            ShoppingCartVM shoppingCartVM = new ShoppingCartVM
            {
                ShoppingCartList = cartItems,
                OrderHeader = new()
            };

            return View(shoppingCartVM);
        }
    }
}
