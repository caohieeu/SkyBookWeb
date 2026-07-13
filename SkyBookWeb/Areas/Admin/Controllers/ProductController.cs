using Microsoft.AspNetCore.Mvc;
using SkyBookWeb.Core.Entities;
using SkyBookWeb.Core.Interfaces;

namespace SkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IGenericRepository<Product> _productRepository;
        public ProductController(IGenericRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
