using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SkyBookWeb.Application;
using SkyBookWeb.Application.Common;
using SkyBookWeb.Application.Implements;
using SkyBookWeb.Core.Entities;
using SkyBookWeb.Core.Specifications;

namespace SkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Upsert()
        {
            IEnumerable<SelectListItem> categoryList = (await _categoryService.GetCategoriesAsync())
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
            ViewData["CategoryList"] = categoryList;
            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[ActionName("Upsert")]
        //public async Task<IActionResult> UpsertPost(Category category)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(category);
        //    }

        //    var result = await _productService.UpdateAsync(category);

        //    if (result is ServiceResult<Category>.Failure failure)
        //    {
        //        ModelState.AddModelError("", failure.Errors);
        //    }
        //    else if (result is ServiceResult<Category>.Success success)
        //    {
        //        TempData["Success"] = "Update category successfully";
        //        return RedirectToAction("Index");
        //    }

        //    return View(category);
        //}
    }
}
