using SkyBookWeb.Application.Common;
using SkyBookWeb.Application.Dtos;
using SkyBookWeb.Core.Entities;
using SkyBookWeb.Core.Specifications;

namespace SkyBookWeb.Application
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllWithSpecification(ProductSpecPrams productSpecPrams);
        Task<ServiceResult<Product>> CreateAsync(Product category);
    }
}
