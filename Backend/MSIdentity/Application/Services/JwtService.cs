using Microsoft.IdentityModel.Tokens;
using MSIdentity.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    private string _jwtToken;
    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    private void CreateToken(string email)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        _jwtToken = jwt;
    }

    public string GetToken(string email)
    {
        CreateToken(email);
        return _jwtToken;
    }

}