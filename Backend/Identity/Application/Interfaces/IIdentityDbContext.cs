using Identity.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Interfaces;

public interface IIdentityDbContext
{
    public DbSet<IdentityUser> IdentityUser { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
