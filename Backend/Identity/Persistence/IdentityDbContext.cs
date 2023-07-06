using Identity.Persistence.EDMs;
using Microsoft.EntityFrameworkCore;

namespace Identity.Persistence;

public class IdentityDbContext : DbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }

    public DbSet<IdentityUser> IdentityUser { get; set; }
}
