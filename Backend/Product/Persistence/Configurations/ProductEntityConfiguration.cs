using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Product.Persistence.Configurations
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Domain.Entities.Product>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Product> builder)
        {
            builder.Property(b => b.Id)
                .IsRequired();
            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(b => b.Description)
                .IsRequired()
                .HasMaxLength(1000);
            builder.Property(b => b.Condition)
             .IsRequired()
             .HasMaxLength(50);
            builder.Property(b => b.Price)
            .IsRequired()
             .HasMaxLength(20);
        }
    }
}
