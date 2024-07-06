using AutoMapper;
using Core.Entities;
using E_CommerceAPI.DTOs;

namespace E_CommerceAPI.Helpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.Image, opt => opt.MapFrom<ProductUrlResolver>());

            CreateMap<Category, CategoryDTO>();

            CreateMap<Brand, BrandDTO>();
        }
        
    }
}
