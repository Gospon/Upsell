using AutoMapper;
using Identity.Application.DTO;
using Identity.Application.Interfaces;
using Identity.Application.Types;
using Identity.Infrastructure.Interfaces;
using Identity.Persistence;
using Identity.Persistence.EDMs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Types;

namespace Identity.Application.Commands;

public record RegisterUserCommand(UserDTO user) : IRequest<Response<string>>;
public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Response<string>>
{
    private readonly IdentityDbContext _identityDbContext;
    private readonly IMapper _mapper;
    private readonly IIdentityRabbitMqProducer _rabbitMqProducer;
    public readonly IJwtService _jwtService;
    public RegisterUserCommandHandler(
        IdentityDbContext identityDbContext,
        IMapper mapper,
        IIdentityRabbitMqProducer rabbitMqProducer,
        IJwtService jwtService
        )
    {
        _identityDbContext = identityDbContext;
        _mapper = mapper;
        _rabbitMqProducer = rabbitMqProducer;
        _jwtService = jwtService;
    }
    public async Task<Response<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        bool userExists = await _identityDbContext.IdentityUser.AnyAsync(u => u.Email == request.user.Email);
        if (userExists)
        {
            return new Response<string>() { Success = false, ErrorMessage = "User already exists." };
        }
        else
        {
            var identityUser = _mapper.Map<IdentityUser>(request.user);

            await _identityDbContext.IdentityUser.AddAsync(identityUser);
            await _identityDbContext.SaveChangesAsync();

            _rabbitMqProducer.SendMessage(new IntegrationEvent<UserDTO>()
            {
                Name = nameof(IdentityEventType.UserRegistered),
                Data = request.user
            });

            var jwtToken = _jwtService.GetToken(request.user);

            return new Response<string>() { Success = true, Data = jwtToken };
        }
    }
}