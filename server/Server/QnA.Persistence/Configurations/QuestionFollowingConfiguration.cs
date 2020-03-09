using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QnA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QnA.Persistence.Configurations
{
    public class QuestionFollowingConfiguration : IEntityTypeConfiguration<QuestionFollowing>
    {
        public void Configure(EntityTypeBuilder<QuestionFollowing> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User)
                .WithMany(x => x.QuestionFollowings)
                .OnDelete(DeleteBehavior.Restrict);
                    
        }
    }
}
