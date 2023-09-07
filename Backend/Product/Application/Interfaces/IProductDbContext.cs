using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;

namespace Product.Application.Interfaces
{
    public interface IProductDbContext
    {
        DbSet<Category> Category { get; }

        DbSet<Domain.Entities.Product> Product { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
