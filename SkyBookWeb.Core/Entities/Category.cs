using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyBookWeb.Core.Entities
{
    public class Category : BaseEntity
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Maximum length of name is 100")]
        public string Name { get; set; } = string.Empty;
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; } = 0;
    }
}
