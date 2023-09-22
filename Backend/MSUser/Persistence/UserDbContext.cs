using Microsoft.EntityFrameworkCore;
using MSUser.Application.Interfaces;
using MSUser.Persistence.Configurations;

namespace MSUser.Persistence;

public class UserDbContext : DbContext, IUserDbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

    public DbSet<Domain.Entities.User> User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new UserEntityConfiguration().Configure(modelBuilder.Entity<Domain.Entities.User>());
    }
}