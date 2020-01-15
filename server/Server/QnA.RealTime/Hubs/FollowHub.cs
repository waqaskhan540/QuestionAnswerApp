using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using QnA.Application.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace QnA.RealTime.Hubs
{
    public class QuestionAnsweredResponse
    {
        public string UserName { get; set; }
        public string QuestionText { get; set; }
        public int QuestionId { get; set; }


    }
    [Authorize]
    public class FollowHub : Hub
    {
        private readonly IDatabaseContext _context;

        public FollowHub(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task QuestionAnswered(int questionId)
        {
            var answeredby = int.Parse(Context.UserIdentifier);
            var question = await _context.Questions.SingleOrDefaultAsync(q => q.Id == questionId);
            if (question != null)
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == answeredby);
                var followers = await _context.QuestionFollowings.Where(q => q.QuestionId == questionId).ToListAsync();

                if (followers.Any() && user != null)
                {
                    var users = followers.Select(x => x.UserId.ToString());
                    var response = new QuestionAnsweredResponse
                    {
                        QuestionId = questionId,
                        UserName = $"{user.FirstName} {user.LastName}",
                        QuestionText = question.QuestionText
                    };
                    await Clients.Users(users.ToList()).SendAsync("QuestionAnswered", response);
                }

            }
        }


    }
}
