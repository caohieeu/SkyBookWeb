using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SkyBookWeb.Core.Entities;
using SkyBookWeb.Core.Interfaces;
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
            var products = await _productRepository.GetAllAsync(x => x.Category);
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            return JsonConvert.SerializeObject(products, serializerSettings);
        }
    }
}
