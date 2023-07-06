using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Types;
using User.Application.DTOs;
using User.Persistence;

namespace User.Application.Commands;

public record CreateUserCommand(UserDTO user) : IRequest<Response<string>>;
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<string>>
{
    private readonly UserDbContext _userDbContext;
    private readonly IMapper _mapper;
    public CreateUserCommandHandler(UserDbContext userDbContext, IMapper mapper)
    {
        _userDbContext = userDbContext;
        _mapper = mapper;
    }
    public async Task<Response<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        bool userExists = await _userDbContext.User.AnyAsync(u => u.Email == request.user.Email);
        if (userExists)
        {
            return new Response<string>() { Success = false, ErrorMessage = "User already exists." };
        }
        else
        {
            var user = _mapper.Map<Persistence.EDMs.User>(request.user);

            await _userDbContext.User.AddAsync(user);
            await _userDbContext.SaveChangesAsync();

            return new Response<string>() { Success = true };
        }
    }
}
