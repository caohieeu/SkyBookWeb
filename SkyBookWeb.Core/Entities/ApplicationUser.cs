using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using SkyBookWeb.Core.Interfaces;

namespace SkyBookWeb.Core.Entities
{
    public class ApplicationUser : IdentityUser, IEntity<string>
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? StreetAddress { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? State { get; set; } = string.Empty;
        public string? PostalCode { get; set; } = string.Empty;
    }
}
