using SkyBookWeb.Application.Interfaces;
using SkyBookWeb.Core.Entities;
using SkyBookWeb.Core.Interfaces;
using SkyBookWeb.Core.Specifications;

namespace SkyBookWeb.Application.Implements
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ClearCartAsync(string userId)
        {
            var cartItems = await GetUserCartItemsAsync(userId);

            if(cartItems != null && cartItems.Any())
            {
                _unitOfWork.Repository<ShoppingCart>().DeleteRange(cartItems);
                await _unitOfWork.Complete();
            }
        }

        public async Task<ShoppingCart> GetCartByIdAsync(int cartId)
        {
            var spec = new ShoppingCartSpecification(cartId: cartId);
            var cart = await _unitOfWork.Repository<ShoppingCart>().GetEntityWithSpec(spec);
            return cart;
        }

        public async Task<int> GetCartCountAsync(string userId)
        {
            var cartItems = await _unitOfWork.Repository<ShoppingCart>().GetAllAsync(x => x.ApplicationId == userId);
            return cartItems.Sum(x => x.Count);
        }

        public async Task<IEnumerable<ShoppingCart>> GetUserCartItemsAsync(string userId)
        {
            var spec = new ShoppingCartSpecification(userId: userId);
            var items = await _unitOfWork.Repository<ShoppingCart>().ListAsync(spec);
            return items;
        }
    }
}
