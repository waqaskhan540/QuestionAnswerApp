using Microsoft.EntityFrameworkCore;
using QnA.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace QnA.Application.Interfaces
{
    public interface IDatabaseContext
    {
        DbSet<AppUser> Users { get; set; }
        DbSet<Question> Questions { get; set; }

        DbSet<Answer> Answers { get; set; }
        DbSet<Draft> Drafts { get; set; }
        DbSet<SavedQuestion> SavedQuestions { get; set; }
        DbSet<QuestionFollowing> QuestionFollowings { get; set; }
        DbSet<RedirectUrl> RedirectUrls { get; set; }
        DbSet<DeveloperApp> DeveloperApps { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
