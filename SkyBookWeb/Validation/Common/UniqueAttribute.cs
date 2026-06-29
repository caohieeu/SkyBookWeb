using System.ComponentModel.DataAnnotations;
using SkyBookWeb.Core.Specifications;

namespace SkyBookWeb.Validation.Common
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class UniqueAttribute : ValidationAttribute
    {
        //private readonly IGenericRepository<>
    }
}
