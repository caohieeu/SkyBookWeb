using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkyBookWeb.Core.Entities
{
    public class Product :BaseEntity
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "ISBN is required")]
        public string ISBN { get; set; } = string.Empty;
        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; } = string.Empty;
        [DisplayName("List Price")]
        [Required(ErrorMessage = "List price is required")]
        [Range(1, 1000, ErrorMessage = "List price must be between 1 and 1000")]
        [Display(AutoGenerateField = false)]
        public double ListPrice { get; set; }
        [Display(Name = "Price For 1-50")]
        [Range(1, 1000, ErrorMessage = "Price For 1-50 must be between 1 and 1000")]
        public double Price { get; set; }
        [DisplayName("Price For 50+")]
        [Range(1, 1000, ErrorMessage = "Price 50+ must be between 1 and 1000")]
        [Display(AutoGenerateField = false)]
        public double Price50 { get; set; }
        [DisplayName("Price For 100+")]
        [Range(1, 1000, ErrorMessage = "Price 100+ must be between 1 and 1000")]
        [Display(AutoGenerateField = false)]
        public double Price100 { get; set; }
        [Display(AutoGenerateField = false)]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [Display(AutoGenerateField = false)]
        public Category Category { get; set; }
        [ValidateNever]
        [DisplayName("Product Image")]
        [Display(AutoGenerateField = false)]
        public string? ImageUrl { get; set; }
    }
}
