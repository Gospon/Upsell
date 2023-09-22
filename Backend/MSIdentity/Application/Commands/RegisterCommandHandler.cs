using MediatR;
using Microsoft.EntityFrameworkCore;
using MSIdentity.Application.Interfaces;
using MSIdentity.Application.Types;
using MSIdentity.Infrastructure.Interfaces;
using MSIdentity.Persistence.Models;

namespace MSIdentity.Application.Commands;

public record RegisterUserCommand(string FirstName, string LastName, string Password, string Email) : IRequest<Response<string>>;
public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Response<string>>
{
    private readonly IIdentityDbContext _context;
    private readonly IIdentityRabbitMqProducer _rabbitMqProducer;
    public readonly IJwtService _jwtService;
    public RegisterUserCommandHandler(
        IIdentityDbContext context,
        IIdentityRabbitMqProducer rabbitMqProducer,
        IJwtService jwtService
        )
    {
        _context = context;
        _rabbitMqProducer = rabbitMqProducer;
        _jwtService = jwtService;
    }
    public async Task<Response<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        bool userExists = await _context.IdentityUser.AnyAsync(u => u.Email == request.Email);
        if (userExists)
        {
            return new Response<string>() { Success = false, ErrorMessage = "User already exists." };
        }
        else
        {
            var identityUser = new IdentityUser()
            {
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            await _context.IdentityUser.AddAsync(identityUser);
            await _context.SaveChangesAsync(cancellationToken);

            _rabbitMqProducer.SendMessage(new IntegrationEvent<RegisterUserCommand>()
            {
                Name = nameof(IdentityEventType.UserRegistered),
                Data = request
            });

            var jwtToken = _jwtService.GetToken(request.Email);

            return new Response<string>() { Success = true, Data = jwtToken };
        }
    }
}