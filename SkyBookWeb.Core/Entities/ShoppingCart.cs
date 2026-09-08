using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SkyBookWeb.Core.Entities
{   
    public class ShoppingCart : BaseEntity
    {
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product Product { get; set; }
        [Range(1, 1000, ErrorMessage = "Please enter a value between 1 and 1000")]
        public int Count { get; set; } = 0;
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        [NotMapped]
        [ValidateNever]
        public double Price
        {
            get
            {
                if (Count <= 0) return 0;
                else if (Count < 50) return Product.Price;
                else if (Count < 100) return Product.Price50;
                else return Product.Price100;
            }
        }
    }
}
