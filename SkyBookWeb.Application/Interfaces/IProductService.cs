using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkyBookWeb.Application.Dtos;
using SkyBookWeb.Core.Specifications;

namespace SkyBookWeb.Application
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllWithSpecification(ProductSpecPrams productSpecPrams);
    }
}
