using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QnA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QnA.Persistence.Configurations
{
    public class SavedQuestionConfiguration : IEntityTypeConfiguration<SavedQuestion>
    {
        public void Configure(EntityTypeBuilder<SavedQuestion> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User)
                    .WithMany(x => x.SavedQuestions)
                    .HasForeignKey(x => x.QuestionId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
