using AutoMapper;
using SkyBookWeb.Application.Dtos;
using SkyBookWeb.Application.Interfaces;
using SkyBookWeb.Core.Entities;
using SkyBookWeb.Core.Interfaces;
using SkyBookWeb.Core.Specifications;

namespace SkyBookWeb.Application.Implements
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IGenericRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IMapper _mapper;
        public ShoppingCartService(IGenericRepository<ShoppingCart> shoppingCartRepository,
            IMapper mapper)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
        }

        public async Task ClearCartAsync(string userId)
        {
            var spec = new ShoppingCartSpecification(userId: userId);
            var cartItems = await _shoppingCartRepository.ListAsync(spec);

            if (cartItems != null && cartItems.Any())
            {
                _shoppingCartRepository.DeleteRange(cartItems);
                await _shoppingCartRepository.SaveChangeAsync();
            }
        }

        public async Task<ShoppingCart> GetCartByIdAsync(int cartId)
        {
            var spec = new ShoppingCartSpecification(cartId: cartId);
            var cart = await _shoppingCartRepository.GetEntityWithSpec(spec);
            return cart;
        }

        public async Task<int> GetCartCountAsync(string userId)
        {
            var cartItems = await _shoppingCartRepository.GetAllAsync(x => x.ApplicationUserId == userId);
            return cartItems.Sum(x => x.Count);
        }

        public async Task<IEnumerable<ShoppingCartDto>> GetUserCartItemsAsync(string userId)
        {
            var spec = new ShoppingCartSpecification(userId: userId);
            var items = await _shoppingCartRepository.ListAsync(spec);
            var result = items.Select(x => _mapper.Map<ShoppingCartDto>(x));
            return result;
        }

        public async Task<ShoppingCart> AddToCartAsync(ShoppingCart shoppingCart)
        {
            var spec = new ShoppingCartSpecification(productId: shoppingCart.ProductId);
            var exixstingItem = await _shoppingCartRepository.GetEntityWithSpec(spec);
            if (exixstingItem != null)
            {
                exixstingItem.Count += shoppingCart.Count;
                await _shoppingCartRepository.SaveChangeAsync();

                return exixstingItem;
            }
            else
            {
                _shoppingCartRepository.Add(shoppingCart);
                await _shoppingCartRepository.SaveChangeAsync();

                return shoppingCart;
            }

        }

        public async Task UpdateCartAsync(ShoppingCart shoppingCart)
        {
            if(shoppingCart.Count <= 0)
            {
                _shoppingCartRepository.Delete(shoppingCart);
            }
            else
            {
                _shoppingCartRepository.Update(shoppingCart);
            }

            await _shoppingCartRepository.SaveChangeAsync();
        }
    }
}
