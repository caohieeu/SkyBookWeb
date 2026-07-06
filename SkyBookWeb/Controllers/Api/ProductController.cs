using Microsoft.AspNetCore.Mvc;
using SkyBookWeb.Core.Entities;
using SkyBookWeb.Core.Specifications;
using System.Text.Json;

namespace SkyBookWeb.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepository;
        public ProductController(IGenericRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("GetAll")]
        public async Task<string> GetAll()
        {
            var products = await _productRepository.GetAllAsync();
            return JsonSerializer.Serialize(products);
        }
    }
}
