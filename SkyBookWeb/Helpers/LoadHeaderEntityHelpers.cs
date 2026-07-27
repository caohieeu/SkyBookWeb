using System.ComponentModel.DataAnnotations;
using System.Reflection;
using SkyBookWeb.Core.Entities;

namespace SkyBookWeb.Helpers
{
    public static class LoadHeaderEntityHelpers<T> where T : BaseEntity
    {
        public static List<string>GetHeaders()
        {
            return typeof(T)
                .GetProperties()
                .Where(x =>
                {
                    var displayAttr = x.GetCustomAttribute<DisplayAttribute>();
                    return displayAttr?.GetAutoGenerateField() != false;
                })
                .Select(x => x.Name)
                .ToList();
        }
    }
}
