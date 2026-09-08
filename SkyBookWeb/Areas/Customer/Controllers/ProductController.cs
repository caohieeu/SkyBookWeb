using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkyBookWeb.Application;
using SkyBookWeb.Application.Interfaces;
using SkyBookWeb.Core.Entities;

namespace SkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IShoppingCartService _shoppingCartService;
        public ProductController(IProductService productService,
            IShoppingCartService shoppingCartService)
        {
            _productService = productService;
            _shoppingCartService = shoppingCartService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Detail(int? productId)
        {
            var product = await _productService.GetProductByIdAsync(productId, includeCategory: true);

            if(product == null)
            {
                return NotFound();
            }

            var productCart = new ShoppingCart
            {
                Product = product,
                ProductId = productId ?? default,
                Count = 1
            };

            return View(productCart);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Detail(ShoppingCart shoppingCart)
        {
            var claimItems = (ClaimsIdentity)User.Identity;
            var userId = claimItems?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            shoppingCart.ApplicationUserId = userId;

            await _shoppingCartService.AddToCartAsync(shoppingCart);

            return RedirectToAction("Detail", new { productId = shoppingCart.ProductId });
        }
    }
}
