using System.Threading.Tasks;
using SkyBookWeb.Application.Common;
using SkyBookWeb.Core.Entities;
using SkyBookWeb.Core.Interfaces;

namespace SkyBookWeb.Application.Implements
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

        public async Task<ServiceResult<Category>> CreateAsync(Category category)
        {
            var isExisted = await _unitOfWork
                    .Repository<Category>()
                    .ExistAsync(c => c.Name.ToLower() == category.Name.ToLower());

            if (isExisted)
            {
                return ServiceResult<Category>.FromFailure("This category name existed");
            }
            else
            {
                _unitOfWork.Repository<Category>().Add(category);
                if (await _unitOfWork.Complete())
                {
                    return ServiceResult<Category>.FromSuccess(category);
                }

                return ServiceResult<Category>.FromFailure(
                    "An error occured while saving the category");
            }
        }

        public Category GetCategoryById(int? id)
        {
            if (id == null)
                return null;

            return _unitOfWork
                .Repository<Category>()
                .GetAsync(x => x.Id == id).Result;
        }

        public async Task<ServiceResult<Category>> UpdateAsync(Category category)
        {
            var isExist = await _unitOfWork
                .Repository<Category>()
                .ExistAsync(c => c.Id == category.Id);

            if (!isExist)
            {
                return ServiceResult<Category>.FromFailure(
                    "This category does not exist");
            }
            else
            {
                _unitOfWork.Repository<Category>().Update(category);
                if (await _unitOfWork.Complete())
                {
                    return ServiceResult<Category>.FromSuccess();
                }
                else
                {
                    return ServiceResult<Category>.FromFailure(
                        "This name category existed or something error");
                }
            }
        }

        public async Task<ServiceResult<Category>> DeleteAsync(int? id)
        {
            if (id == null)
                return ServiceResult<Category>.FromFailure("Id can not be null");

            var currentCategory = await _unitOfWork
                    .Repository<Category>()
                    .GetAsync(c => c.Id == id);

            if (currentCategory == null)
            {
                return ServiceResult<Category>.FromFailure("Deleting this category is invalid");
            }

            _unitOfWork.Repository<Category>().Delete(currentCategory);
            if (await _unitOfWork.Complete())
            {
                return ServiceResult<Category>.FromSuccess(currentCategory);
            }
            else
            {
                return ServiceResult<Category>.FromFailure("Something went wrong");
            }
        }
    }
}
