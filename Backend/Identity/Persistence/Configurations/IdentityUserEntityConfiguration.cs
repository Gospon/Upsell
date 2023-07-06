using Identity.Persistence.EDMs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Persistence.Configurations;

public class IdentityUserEntityConfiguration : IEntityTypeConfiguration<IdentityUser>
{
    public void Configure(EntityTypeBuilder<IdentityUser> builder)
    {
        builder.Property(b => b.Id).IsRequired();
        builder.Property(b => b.Email).IsRequired();
        builder.Property(b => b.PasswordHash).IsRequired();
    }
}
