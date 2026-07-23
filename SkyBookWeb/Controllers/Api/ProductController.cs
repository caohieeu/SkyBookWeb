using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SkyBookWeb.Application;
using SkyBookWeb.Core.Specifications;

namespace SkyBookWeb.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAll")]
        public async Task<string> GetAll([FromQuery] ProductSpecPrams productSpecPrams)
        {
            var products = await _productService.GetAllWithSpecification(productSpecPrams);
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            return JsonConvert.SerializeObject(products.ToList(), serializerSettings);
        }
    }
}
