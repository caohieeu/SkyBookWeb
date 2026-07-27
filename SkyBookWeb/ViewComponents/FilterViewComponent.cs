using Microsoft.AspNetCore.Mvc;
using SkyBookWeb.Application.Dtos;
using SkyBookWeb.Core.Entities;
using SkyBookWeb.Helpers;

namespace SkyBookWeb.ViewComponents
{
    public class FilterViewComponent : ViewComponent
    {
        public FilterViewComponent() { }
        public IViewComponentResult Invoke()
        {
            var result = new FilterDto
            {
                OrderBySelections = LoadHeaderEntityHelpers<Product>
                .GetHeaders()
                .Select(x => new OrderBySelection
                {
                    Title = x,
                    Value = x.ToLower()
                }).ToList()
            };
            return View(result);
        }
    }
}
