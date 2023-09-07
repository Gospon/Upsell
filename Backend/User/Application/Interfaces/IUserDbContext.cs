using Microsoft.EntityFrameworkCore;

namespace User.Application.Interfaces
{
    public interface IUserDbContext
    {
        public DbSet<Domain.Entities.User> User { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
