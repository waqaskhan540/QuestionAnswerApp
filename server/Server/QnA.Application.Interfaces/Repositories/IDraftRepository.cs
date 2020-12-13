using QnA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Application.Interfaces.Repositories
{
    public interface IDraftRepository
    {
        void Update(Draft draft);
        Task AddAsync(Draft draft);
        
        Task<Draft> GetByQuestionAndUser(int userId, int questionId);
    }
}
