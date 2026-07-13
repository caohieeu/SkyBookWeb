using SkyBookWeb.Application.ICustomServices;
using SkyBookWeb.Core.Entities;
using SkyBookWeb.Core.Interfaces;

namespace SkyBookWeb.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IGenericRepository<Category> categoryRepository,
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var categories = await _unitOfWork.Repository<Category>().GetAllAsync();
            return categories;
        }
    }
}
