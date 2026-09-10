using SkyBookWeb.Application.Dtos;
using SkyBookWeb.Core.Entities;

namespace SkyBookWeb.Models.ViewModels
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCartDto> ShoppingCartList { get; set; }
        public OrderHeader OrderHeader { get; set; }
        public double GetTotalPrice()
        {
            var total = 0.0;
            foreach(var cartItem in ShoppingCartList)
            {
                total += (cartItem.Count * cartItem.Price);
            }
            return total;
        }
    }
}
