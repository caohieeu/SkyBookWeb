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
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IGenericRepository<Category> categoryRepository,
            IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> CreatePost(Category category)
        {
            if(ModelState.IsValid)
            {
                _categoryRepository.Add(category);
                if (await _unitOfWork.Complete())
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
    }
}
