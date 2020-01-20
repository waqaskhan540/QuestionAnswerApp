using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QnA.Domain.Entities;

namespace QnA.Persistence.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder
              .HasIndex(x => x.Email)
              .IsUnique()
              ;


            builder.HasMany(x => x.Apps).WithOne(x => x.Developer).HasForeignKey(x => x.UserId);



        }
    }
}
