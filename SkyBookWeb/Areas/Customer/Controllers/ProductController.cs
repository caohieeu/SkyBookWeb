using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SkyBookWeb.Application;

namespace SkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Detail(int? productId)
        {
            var product = await _productService.GetProductByIdAsync(productId, includeCategory: true);
            return View(product);
        }
    }
}
