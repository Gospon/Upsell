using Microsoft.EntityFrameworkCore;
using User.Persistence.Configurations;

namespace User.Persistence;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

    public DbSet<EDMs.User> User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new UserEntityConfiguration().Configure(modelBuilder.Entity<EDMs.User>());
    }
}