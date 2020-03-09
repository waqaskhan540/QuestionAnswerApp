using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QnA.Domain.Entities;

namespace QnA.Persistence.Configurations
{
    public class DeveloperAppConfiguration : IEntityTypeConfiguration<DeveloperApp>
    {
        public void Configure(EntityTypeBuilder<DeveloperApp> builder)
        {
            builder.HasKey(x => x.AppId);
            builder.Property(x => x.AppName)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(x => x.RedirectUrls)
                .WithOne(x => x.App)
                .HasForeignKey(x => x.AppId);
        }
    }
}
