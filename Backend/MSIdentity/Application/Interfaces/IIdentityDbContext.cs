using Microsoft.EntityFrameworkCore;
using MSIdentity.Persistence.Models;

namespace MSIdentity.Application.Interfaces;

public interface IIdentityDbContext
{
    public DbSet<IdentityUser> IdentityUser { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
