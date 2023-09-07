using Microsoft.EntityFrameworkCore;
using Product.Application.Interfaces;
using Product.Domain.Entities;
using Product.Persistence.Configurations;

namespace Product.Persistence;

public class ProductDbContext : DbContext, IProductDbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

    public DbSet<Category> Category { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new CategoryEntityConfiguration().Configure(modelBuilder.Entity<Category>());
        new ProductEntityConfiguration().Configure(modelBuilder.Entity<Domain.Entities.Product>());
    }
}
