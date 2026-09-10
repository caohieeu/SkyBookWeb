using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyBookWeb.Application.Dtos
{
    public class ShoppingCartDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public string ApplicationUserId { get; set; } = string.Empty;
        public double Price { get; set; }
        public ProductDto Product { get; set; } = new();
    }
}
