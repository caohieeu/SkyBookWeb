using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkyBookWeb.Application.Common;
using SkyBookWeb.Core.Entities;

namespace SkyBookWeb.Application
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Category GetCategoryById(int? id);
        Task<ServiceResult<Category>> CreateAsync(Category category);
        Task<ServiceResult<Category>> UpdateAsync(Category category);
        Task<ServiceResult<Category>> DeleteAsync(int? id);
    }
}
