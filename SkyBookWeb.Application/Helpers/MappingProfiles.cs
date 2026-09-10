using AutoMapper;
using SkyBookWeb.Application.Dtos;
using SkyBookWeb.Application.Interfaces;
using SkyBookWeb.Core.Entities;

namespace SkyBookWeb.Application
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>()
                .AfterMap<ProductMapping>();
            CreateMap<ShoppingCart, ShoppingCartDto>();
        }

        public class ProductMapping : IMappingAction<Product, ProductDto>
        {
            private readonly IFileService _fileService;
            public ProductMapping(IFileService fileService)
            {
                _fileService = fileService;
            }
            public void Process(Product source, ProductDto destination, ResolutionContext context)
            {
                destination.Category = source.Category.Name;
                destination.ImageUrl = _fileService.GetImagePath(source?.ImageUrl);
            }
        }
    }
}
