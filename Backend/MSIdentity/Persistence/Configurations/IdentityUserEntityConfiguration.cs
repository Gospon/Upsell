using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MSIdentity.Persistence.Models;

namespace MSIdentity.Persistence.Configurations;

public class IdentityUserEntityConfiguration : IEntityTypeConfiguration<IdentityUser>
{
    public void Configure(EntityTypeBuilder<IdentityUser> builder)
    {
        builder.Property(b => b.Id).IsRequired();
        builder.Property(b => b.Email).IsRequired();
        builder.Property(b => b.PasswordHash).IsRequired();
    }
}
