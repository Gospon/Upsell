using AutoMapper;
using Product.Application.DTO;
using Product.Domain.Entities;

namespace Product.Persistence.Mapper;

public class IdentityMappingProfile : Profile
{
    public IdentityMappingProfile()
    {
        CreateMap<CategoryDTO, Category>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ParentCategoryId, opt => opt.MapFrom(src => src.ParentCategoryId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));
    }
}
