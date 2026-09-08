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
        public ShoppingCartSpecification(string? userId = "", int? cartId = 0) : base(
            x => ((string.IsNullOrEmpty(userId) || userId == x.ApplicationUserId) &&
            (cartId == 0 || cartId == x.Id)))
        {
            AddInclude(x => x.Product);
        }
    }
}
