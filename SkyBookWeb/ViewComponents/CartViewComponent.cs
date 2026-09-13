using Microsoft.AspNetCore.Mvc;
using SkyBookWeb.Application.Interfaces;
using System.Net.WebSockets;

namespace SkyBookWeb.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IUserIdentityService _userIdentityService;
        public CartViewComponent(IShoppingCartService shoppingCartService,
            IUserIdentityService userIdentityService)
        {
            _shoppingCartService = shoppingCartService;
            _userIdentityService = userIdentityService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var a = _userIdentityService.GetUserId();
            var b = User.Identity;
            return View(await _shoppingCartService
                .GetCartCountAsync(_userIdentityService.GetUserId() ?? string.Empty));
        }
    }
}
