using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QnA.Domain.Entities;

namespace QnA.Persistence.Configurations
{
    public class RedirectUrlConfiguration : IEntityTypeConfiguration<RedirectUrl>
    {
        public void Configure(EntityTypeBuilder<RedirectUrl> builder)
        {
            builder.HasKey(x => new { x.AppId, x.RedirectUri });
            builder.HasOne(x => x.App).WithMany(x => x.RedirectUrls);

        }
    }
}
