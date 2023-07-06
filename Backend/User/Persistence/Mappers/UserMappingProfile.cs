using AutoMapper;
using User.Application.DTOs;

namespace User.Persistence.Mappers;
public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserDTO, EDMs.User>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
    }
}