using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace QnA.RealTime.Hubs
{
    [Authorize]
    public class FollowHub : Hub
    {
        public async Task QuestionAnswered(int questionId)
        {
            var answeredBy = Context.UserIdentifier;
            await Clients.All.SendAsync("QuestionAnswered", answeredBy, questionId);
        }
    }
}
