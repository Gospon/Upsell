using Microsoft.EntityFrameworkCore;
using User.Application.Interfaces;
using User.Persistence.Configurations;

namespace User.Persistence;

public class UserDbContext : DbContext, IUserDbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

    public DbSet<Domain.Entities.User> User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new UserEntityConfiguration().Configure(modelBuilder.Entity<Domain.Entities.User>());
    }
}