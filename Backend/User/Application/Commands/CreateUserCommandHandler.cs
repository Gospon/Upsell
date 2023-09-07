using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Types;
using User.Application.Interfaces;

namespace User.Application.Commands;

public record CreateUserCommand(Domain.Entities.User user) : IRequest<Response<string>>;
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<string>>
{
    private readonly IUserDbContext _context;
    public CreateUserCommandHandler(IUserDbContext context)
    {
        _context = context;
    }
    public async Task<Response<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        bool userExists = await _context.User.AnyAsync(u => u.Email == request.user.Email);
        if (userExists)
        {
            return new Response<string>() { Success = false, ErrorMessage = "User already exists." };
        }
        else
        {
            await _context.User.AddAsync(request.user);
            await _context.SaveChangesAsync(cancellationToken);

            return new Response<string>() { Success = true };
        }
    }
}
