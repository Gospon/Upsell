namespace MSIdentity.Application.Interfaces;

public interface IJwtService
{
    string GetToken(string email);
}
