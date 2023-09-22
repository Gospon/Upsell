using Microsoft.EntityFrameworkCore;
using MSIdentity.Application.Interfaces;
using MSIdentity.Persistence.Models;

namespace MSIdentity.Persistence;

public class IdentityDbContext : DbContext, IIdentityDbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }

    public DbSet<IdentityUser> IdentityUser { get; set; }
}
