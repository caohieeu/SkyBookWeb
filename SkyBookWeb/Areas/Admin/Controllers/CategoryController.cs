using Microsoft.AspNetCore.Mvc;
using SkyBookWeb.Application.Common;
using SkyBookWeb.Application;
using SkyBookWeb.Core.Entities;

namespace SkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(
            ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetCategoriesAsync());
        }
        public IActionResult Create()
        {
            Category category = new Category();
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> CreatePost(Category category)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            var result = await _categoryService.CreateAsync(category);

            if (result is ServiceResult<Category>.Failure failure)
            {
                ModelState.AddModelError("", failure.Errors);
            }
            else if (result is ServiceResult<Category>.Success success)
            {
                TempData["Success"] = "Category add successfully";
                return RedirectToAction("Index");
            }
            
            return View();
        }
        public IActionResult Update(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var currentCategory = _categoryService.GetCategoryById(id);

            if (currentCategory == null)
            {
                return NotFound();
            }

            return View(currentCategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Update")]
        public async Task<IActionResult> UpdatePost(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            var result = await _categoryService.UpdateAsync(category);

            if(result is ServiceResult<Category>.Failure failure)
            {
                ModelState.AddModelError("", failure.Errors);
            }
            else if(result is ServiceResult<Category>.Success success)
            {
                TempData["Success"] = "Update category successfully";
                return RedirectToAction("Index");
            }

            return View(category);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var currentCategory = _categoryService.GetCategoryById(id);

            if (currentCategory == null)
            {
                return NotFound();
            }

            return View(currentCategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            var result = await _categoryService.DeleteAsync(id);

            if(result is ServiceResult<Category>.Failure failure)
            {
                ModelState.AddModelError("", failure.Errors);
            }
            else if(result is ServiceResult<Category>.Success success)
            {
                TempData["Success"] = "Delete category successfully";
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
