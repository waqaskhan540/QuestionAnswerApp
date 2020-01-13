using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QnA.Domain.Entities;
using QnA.Security;

namespace QnA.Persistence.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder
              .HasIndex(x => x.Email)
              .IsUnique();

            var hashGenerator = new HashGenerator();

        }
    }
}
