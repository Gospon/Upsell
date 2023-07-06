using Identity.Application.DTO;

namespace Identity.Application.Interfaces;

public interface IJwtService
{
    string GetToken(UserDTO user);
}
