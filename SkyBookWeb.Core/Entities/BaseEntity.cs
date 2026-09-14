using System.ComponentModel.DataAnnotations;
using SkyBookWeb.Core.Interfaces;

namespace SkyBookWeb.Core.Entities
{
    public class BaseEntity
    {
        [Key]
        [Display(AutoGenerateField = false)]
        public int Id { get; set; }
    }
}
