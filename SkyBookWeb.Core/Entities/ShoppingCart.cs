using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public int Count { get; set; }
        public string ApplicationId { get; set; }
        [ForeignKey("ApplicationId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        [NotMapped]
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
