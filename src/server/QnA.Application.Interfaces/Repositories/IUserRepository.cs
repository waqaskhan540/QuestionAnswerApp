using QnA.Domain.Entities;
using System.Threading.Tasks;

namespace QnA.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<bool> UserExists(string email);
        Task AddAsync(QnAUser user);
        Task<QnAUser> GetUserByEmail(string email);
        void Update(QnAUser user);
    }
}
