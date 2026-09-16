using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkyBookWeb.Application;
using SkyBookWeb.Application.Interfaces;
using SkyBookWeb.Core.Entities;
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
        private readonly IApplicationUserService _applicationUserService;
        public CartController(ILogger<HomeController> logger,
            IShoppingCartService shoppingCartService,
            IUserIdentityService userIdentityService,
            IApplicationUserService applicationUserService)
        {
            _logger = logger;
            _shoppingCartService = shoppingCartService;
            _userIdentityService = userIdentityService;
            _applicationUserService = applicationUserService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userIdentityService.GetUserId();
            var cartItems = await _shoppingCartService.GetUserCartItemsAsync(userId);
            var user = await _applicationUserService.GetUserByIdAsync(userId);

            ShoppingCartVM shoppingCartVM = new ShoppingCartVM
            {
                ShoppingCartList = cartItems,
                OrderHeader = new()
            };

            if (user != null && !string.IsNullOrEmpty(userId))
            {
                shoppingCartVM.OrderHeader = new OrderHeader
                {
                    ApplicationUser = user,
                    ApplicationUserId = userId,
                    PhoneNumber = user.PhoneNumber,
                    StreetAddress = user.StreetAddress,
                    City = user.City,
                    State = user.State,
                    PostalCode = user.PostalCode,
                    Name = user.Name
                };
            }

            return View(shoppingCartVM);
        }
        public async Task<IActionResult> Plus(int cartId)   
        {
            var cart = await _shoppingCartService.GetCartByIdAsync(cartId);

            if(cart != null)
            {
                cart.Count++;
                await _shoppingCartService.UpdateCartAsync(cart);
            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Minus(int cartId)
        {
            var cart = await _shoppingCartService.GetCartByIdAsync(cartId);

            if (cart != null)
            {
                cart.Count--;
                await _shoppingCartService.UpdateCartAsync(cart);
            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Remove(int cartId)
        {
            if(cartId != 0)
            {
                await _shoppingCartService.RemoveCartItemAsync(cartId);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
