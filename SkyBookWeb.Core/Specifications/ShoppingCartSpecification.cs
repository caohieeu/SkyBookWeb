using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkyBookWeb.Core.Entities;

namespace SkyBookWeb.Core.Specifications
{
    public class ShoppingCartSpecification : BaseSpecification<ShoppingCart>
    {
        public ShoppingCartSpecification(string? userId = "", int? cartId = 0, int? productId = 0) : base(
            x => ((string.IsNullOrEmpty(userId) || userId == x.ApplicationUserId) &&
            (cartId == 0 || cartId == x.Id) &&
            (productId == 0 || productId == x.ProductId)))
        {
            AddInclude(x => x.Product);
            AddInclude(x => x.Product.Category);
        }
    }
}
