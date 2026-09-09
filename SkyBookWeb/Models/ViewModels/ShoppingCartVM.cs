using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SkyBookWeb.Core.Entities;

namespace SkyBookWeb.Models.ViewModels
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
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
