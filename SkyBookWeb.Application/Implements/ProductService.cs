using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SkyBookWeb.Application.Common;
using SkyBookWeb.Application.Dtos;
using SkyBookWeb.Core.Entities;
using SkyBookWeb.Core.Interfaces;
using SkyBookWeb.Core.Specifications;

namespace SkyBookWeb.Application.Implements
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IGenericRepository<Product> productRepository,
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<ProductDto>> GetAllWithSpecification(ProductSpecPrams productSpecPrams)
        {
            productSpecPrams.IncludeCategory = true;
            var spec = new ProductWithSecification(productSpecPrams);

            var products = await _productRepository.ListAsync(spec);
            var result = products.Select(x => _mapper.Map<ProductDto>(x));

            return result;
        }
        public async Task<ServiceResult<Product>> UpsertAsync(Product product)
        {
            var isExisted = await _unitOfWork
                    .Repository<Product>()
                    .ExistAsync(c => c.Title.ToLower() == product.Title.ToLower());

            if (product.Id == 0)
            {
                if (isExisted)
                {
                    return ServiceResult<Product>.FromFailure("This product name existed");
                }
                _unitOfWork.Repository<Product>().Add(product);
            }
            else
            {
                _unitOfWork.Repository<Product>().Update(product);
            }
            if (await _unitOfWork.Complete())
            {
                return ServiceResult<Product>.FromSuccess(product);
            }

            return ServiceResult<Product>.FromFailure(
                "An error occured while saving the product");
        }
        public async Task<ServiceResult<Product>> DeleteAsync(int? id)
        {
            if (id == null)
                return ServiceResult<Product>.FromFailure("Id can not be null");

            var currentProduct = await _unitOfWork
                    .Repository<Product>()
                    .GetAsync(c => c.Id == id);

            if (currentProduct == null)
            {
                return ServiceResult<Product>.FromFailure("Deleting this product is invalid");
            }

            _unitOfWork.Repository<Product>().Delete(currentProduct);
            if (await _unitOfWork.Complete())
            {
                return ServiceResult<Product>.FromSuccess(currentProduct);
            }
            else
            {
                return ServiceResult<Product>.FromFailure("Something went wrong");
            }
        }

        public async Task<Product> GetProductByIdAsync(int? id, bool includeCategory = false)
        {
            if (id == null)
                return null;

            var spec = new ProductWithSecification(new ProductSpecPrams() { Id = id, IncludeCategory = true });
            return await _unitOfWork
                .Repository<Product>()
                .GetEntityWithSpec(spec);
        }
    }
}
