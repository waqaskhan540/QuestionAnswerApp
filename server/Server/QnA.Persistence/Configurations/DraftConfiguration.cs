using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QnA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QnA.Persistence.Configurations
{
    public class DraftConfiguration : IEntityTypeConfiguration<Draft>
    {
        public void Configure(EntityTypeBuilder<Draft> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User)
                        .WithMany(x => x.Drafts)
                        .HasForeignKey(x => x.UserId)
                        .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}
