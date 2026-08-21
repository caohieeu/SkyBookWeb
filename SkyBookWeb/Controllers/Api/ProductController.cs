using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SkyBookWeb.Application;
using SkyBookWeb.Application.Common;
using SkyBookWeb.Application.Interfaces;
using SkyBookWeb.Core.Entities;
using SkyBookWeb.Core.Specifications;
using SkyBookWeb.Utilty;

namespace SkyBookWeb.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Constant.RoleAdmin)]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IFileService _fileService;
        public ProductController(IProductService productService,
            IFileService fileService)
        {
            _productService = productService;
            _fileService = fileService;
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<string> GetAll([FromQuery] ProductSpecPrams productSpecPrams)
        {
            var products = await _productService.GetAllWithSpecification(productSpecPrams);
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            return JsonConvert.SerializeObject(products.ToList(), serializerSettings);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<string> DeletePost(int? id)
        {
            var result = await _productService.DeleteAsync(id);

            if (result is ServiceResult<Product>.Failure failure)
            {
                return JsonConvert.SerializeObject(new { success = false, message = failure.Errors });
            }
            else if (result is ServiceResult<Product>.Success success)
            {
                _fileService.RemoveImage(success?.entity?.ImageUrl);

                return JsonConvert.SerializeObject(new { success = true, message = "Delete successfully" });
            }
            else
            {
                return JsonConvert.SerializeObject(new { success = false, message = "Something went wrong!" });
            }
        }
    }
}
