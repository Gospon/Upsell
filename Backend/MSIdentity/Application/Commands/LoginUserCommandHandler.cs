using MediatR;
using MSIdentity.Application.Interfaces;
using MSIdentity.Application.Types;

namespace MSIdentity.Application.Commands;

public record LoginUserCommand(string Email, string Password) : IRequest<Response<string>>;
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Response<string>>
{
    private readonly IIdentityDbContext _context;
    private readonly IJwtService _jwtService;
    public LoginUserCommandHandler(IIdentityDbContext context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }
    public async Task<Response<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = _context.IdentityUser.FirstOrDefault(u => u.Email == request.Email);
        if (user is not null)
        {
            var passwordMatch = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);


            if (passwordMatch)
            {
                var jwtToken = _jwtService.GetToken(request.Email);
                return new Response<string>() { Success = true, Data = jwtToken };
            }
        }


        return new Response<string>() { Success = false, ErrorMessage = "Bad Credentials" };
    }
}