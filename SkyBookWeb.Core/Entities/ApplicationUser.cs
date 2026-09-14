using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using SkyBookWeb.Core.Interfaces;

namespace SkyBookWeb.Core.Entities
{
    public class ApplicationUser : IdentityUser, IEntity<string>
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public int? PostalCode { get; set; }
    }
}
