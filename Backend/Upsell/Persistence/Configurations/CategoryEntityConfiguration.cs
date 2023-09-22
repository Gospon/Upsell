using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;

namespace Product.Persistence.Configurations
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(b => b.Id).IsRequired();
            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("varchar");
        }
    }
}
