using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
        public ProductService(IGenericRepository<Product> productRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductDto>> GetAllWithSpecification(ProductSpecPrams productSpecPrams)
        {
            var spec = new ProductWithSecification(productSpecPrams);

            var products = await _productRepository.ListAsync(spec);
            var result = products.Select(x => _mapper.Map<ProductDto>(x));

            return result;
        }
    }
}
