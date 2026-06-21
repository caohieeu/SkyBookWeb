using System.ComponentModel.DataAnnotations;

namespace SkyBookWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Order { get; set; }
    }
}
