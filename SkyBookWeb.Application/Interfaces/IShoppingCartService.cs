using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkyBookWeb.Core.Entities;

namespace SkyBookWeb.Application.Interfaces
{
    public interface IShoppingCartService
    {
        Task<ShoppingCart> GetCartByIdAsync(int cartId);
        Task<IEnumerable<ShoppingCart>> GetUserCartItemsAsync(string userId);
        Task<int> GetCartCountAsync(string userId);
        Task ClearCartAsync(string userId);
        Task<ShoppingCart> AddToCartAsync(ShoppingCart shoppingCart);
        Task UpdateCartAsync(ShoppingCart shoppingCart);
    }
}
