using SkyBookWeb.Application.Common;
using SkyBookWeb.Application.Dtos;
using SkyBookWeb.Core.Entities;
using SkyBookWeb.Core.Specifications;

namespace SkyBookWeb.Application
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllWithSpecification(ProductSpecPrams productSpecPrams);
        Task<Product> GetProductByIdAsync(int? id, bool includeCategory = false);
        Task<ServiceResult<Product>> UpsertAsync(Product category);
        Task<ServiceResult<Product>> DeleteAsync(int? id);
    }
}
