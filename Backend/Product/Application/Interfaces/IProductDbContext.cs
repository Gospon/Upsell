using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;

namespace Product.Application.Interfaces
{
    public interface IProductDbContext
    {
        DbSet<Category> Category { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
