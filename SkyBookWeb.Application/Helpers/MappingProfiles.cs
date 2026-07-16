using AutoMapper;
using SkyBookWeb.Application.Dtos;
using SkyBookWeb.Core.Entities;

namespace SkyBookWeb.Application
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.Category, opt => opt.MapFrom(p => p.Category.Name));
        }
    }
}
