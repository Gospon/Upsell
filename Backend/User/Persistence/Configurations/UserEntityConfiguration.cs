using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace User.Persistence.Configurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<Domain.Entities.User>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.User> builder)
        {
            builder.Property(b => b.Id).IsRequired();
            builder.Property(b => b.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("varchar");
            builder.Property(b => b.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("varchar");
            builder.Property(b => b.Email).IsRequired();
        }
    }
}
