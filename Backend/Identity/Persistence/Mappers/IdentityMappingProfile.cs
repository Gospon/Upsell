using AutoMapper;
using Identity.Application.DTO;
using Identity.Persistence.EDMs;

namespace Identity.Persistence.Mappers;

public class IdentityMappingProfile : Profile
{
    public IdentityMappingProfile()
    {
        CreateMap<UserDTO, IdentityUser>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)));
    }
}
