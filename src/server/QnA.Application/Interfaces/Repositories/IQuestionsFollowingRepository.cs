using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Application.Interfaces.Repositories
{
    public interface IQuestionsFollowingRepository
    {
        Task<bool> QuestionsHasFollowers(int questionId);
    }
}
