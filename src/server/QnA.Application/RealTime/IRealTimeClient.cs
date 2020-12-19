using System.Threading.Tasks;

namespace QnA.Application.RealTime
{
    public interface IRealTimeClient
    {
        Task NotifyOnQuestionAnswered(int questionId, int userId);
    }
}
