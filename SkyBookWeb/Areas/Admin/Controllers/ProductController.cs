using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SkyBookWeb.Application;
using SkyBookWeb.Application.Interfaces;
using SkyBookWeb.Core.Entities;
using SkyBookWeb.Models.ViewModels;

namespace SkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _uploadService;
        public ProductController(IProductService productService, 
            ICategoryService categoryService,
            IWebHostEnvironment webHostEnvironment,
            IFileService uploadService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
            _uploadService = uploadService;
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

            var viewModel = new ProductVM
            {
                Product = new Product(),
                CategoryList = categoryList
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Upsert")]
        public async Task<IActionResult> UpsertPost(Product product, IFormFile fileImage)
        {
            if (!ModelState.IsValid)
            {
                return View(new ProductVM());
            }

            var wwwRootPath = _webHostEnvironment.WebRootPath;
            var fileName = Guid.NewGuid().ToString() + fileImage.FileName;
            var filePath = Path.Combine("images", "product");
            var finalPath = Path.Combine(wwwRootPath, filePath);

            if(!Directory.Exists(finalPath))
            {
                Directory.CreateDirectory(finalPath);
            }

            using(var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
            {
                fileImage.CopyTo(fileStream);
            }

            product.ImageUrl = Path.Combine(@"\", finalPath, fileName);

            return View(new ProductVM());
        }
    }
}
