using Identity.Application.DTO;
using Identity.Application.Interfaces;
using Identity.Persistence;
using MediatR;
using SharedKernel.Types;

namespace Identity.Application.Commands;

public record LoginUserCommand(UserDTO user) : IRequest<Response<string>>;
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Response<string>>
{
    private readonly IdentityDbContext _identityDbContext;
    private readonly IJwtService _jwtService;
    public LoginUserCommandHandler(IdentityDbContext identityDbContext, IJwtService jwtService)
    {
        _identityDbContext = identityDbContext;
        _jwtService = jwtService;
    }
    public async Task<Response<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = _identityDbContext.IdentityUser.FirstOrDefault(u => u.Email == request.user.Email);
        if (user is not null)
        {
            var passwordMatch = BCrypt.Net.BCrypt.Verify(request.user.Password, user.PasswordHash);


            if (passwordMatch)
            {
                var jwtToken = _jwtService.GetToken(request.user);
                return new Response<string>() { Success = true, Data = jwtToken };
            }
        }


        return new Response<string>() { Success = false, ErrorMessage = "Bad Credentials" };
    }
}