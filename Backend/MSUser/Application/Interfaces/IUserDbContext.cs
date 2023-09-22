using Microsoft.EntityFrameworkCore;

namespace MSUser.Application.Interfaces;

public interface IUserDbContext
{
    public DbSet<Domain.Entities.User> User { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
