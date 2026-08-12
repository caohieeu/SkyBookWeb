using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SkyBookWeb.Application;
using SkyBookWeb.Application.Common;
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
        private readonly IFileService _fileService;
        public ProductController(IProductService productService, 
            ICategoryService categoryService,
            IWebHostEnvironment webHostEnvironment,
            IFileService fileService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _fileService = fileService;
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

            if (fileImage != null)
            {
                product.ImageUrl = await _fileService.UploadAsync(fileImage, Path.Combine("images", "products"));
            }

            var result = await _productService.CreateAsync(product);

            if (result is ServiceResult<Product>.Failure failure)
            {
                ModelState.AddModelError("", failure.Errors);
            }
            else if (result is ServiceResult<Product>.Success success)
            {
                TempData["Success"] = "Product added successfully";
                return RedirectToAction("Index");
            }

            return View(new ProductVM());
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var currentProduct = await _productService.GetProductById(id);

            if (currentProduct == null)
                return NotFound();

            return View(currentProduct);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            var result = await _productService.DeleteAsync(id);

            if (result is ServiceResult<Product>.Failure failure)
            {
                ModelState.AddModelError("", failure.Errors);
            }
            else if (result is ServiceResult<Product>.Success success)
            {
                TempData["Success"] = "Delete product successfully";
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
