using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SkyBookWeb.Core.Entities;
using SkyBookWeb.Core.Interfaces;
using SkyBookWeb.Core.Specifications;
using SkyBookWeb.Infrastructure.Data;

namespace SkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IGenericRepository<Category> categoryRepository,
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _unitOfWork.Repository<Category>().GetAllAsync();
            return View(categories);
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
            if(ModelState.IsValid)
            {
                if (await _unitOfWork
                    .Repository<Category>()
                    .ExistAsync(c => c.Name.ToLower() == category.Name.ToLower()))
                {
                    ModelState.AddModelError("", "This category name existed");
                }
                else
                {
                    _unitOfWork.Repository<Category>().Add(category);
                    if (await _unitOfWork.Complete())
                    {
                        TempData["Success"] = "Category add successfully";
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }
        public async Task<IActionResult> Update(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var currentCategory = await _unitOfWork
                .Repository<Category>()
                .GetAsync(x => x.Id == id);

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
            if (ModelState.IsValid)
            {
                if(!await _unitOfWork.Repository<Category>().ExistAsync(c => c.Id == category.Id))
                {
                    ModelState.AddModelError("", "This category does not exist");
                }
                else
                {
                    _unitOfWork.Repository<Category>().Update(category);
                    if (await _unitOfWork.Complete())
                    {
                        TempData["Success"] = "Update category successfully";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "This name category existed or something error");
                    }
                }
            }
            return View(category);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var currentCategory = await _unitOfWork
                .Repository<Category>()
                .GetAsync(x => x.Id == id);

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
            var currentCategory = await _unitOfWork
                    .Repository<Category>()
                    .GetAsync(c => c.Id == id);
            if (currentCategory == null)
            {
                ModelState.AddModelError("", "Deleting this category is invalid");
            }
            else
            {
                _unitOfWork.Repository<Category>().Delete(currentCategory);
                if (await _unitOfWork.Complete())
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
    }
}
