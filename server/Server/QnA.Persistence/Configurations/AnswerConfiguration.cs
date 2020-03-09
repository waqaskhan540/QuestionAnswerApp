using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QnA.Domain.Entities;

namespace QnA.Persistence.Configurations
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasKey(x => x.AnswerId);
            builder
                .Property(x => x.AnswerMarkup)
                .IsRequired();
            builder
                .Property(x => x.DateTime)
                .IsRequired();

            builder
                .HasOne(x => x.Question)
                .WithMany(x => x.Answers)
                .HasForeignKey( x => x.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Answers)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
                    
                
                

        }
    }
}
