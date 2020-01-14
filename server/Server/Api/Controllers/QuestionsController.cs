using Api.ApiModels;
using Api.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QnA.Application.Feed.Queries;
using QnA.Application.Questions.Commands;
using QnA.Application.Questions.Queries;
using System.Threading.Tasks;


namespace Api.Controllers
{

    public class QuestionsController : Controller
    {
        private readonly IMediator _mediator;
        
        public QuestionsController(IMediator mediator)
        {
            _mediator = mediator;            
        }

        [HttpPost("api/questions")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody]QuestionViewModel model)
        {
            var userId = HttpContext.GetLoggedUserId();
            var questionId = await _mediator.Send(new AddQuestionCommand(userId, model.QuestionText));

            return await GetQuestion(questionId);
        }

        [HttpGet("api/feed/{page:int}")]
        public async Task<IActionResult> GetFeed(int page = 1)
        {
            var questions = await _mediator.Send(new GetFeedByPageQuery(page: page));
            return Ok(BaseResponse.Ok(questions));
        }

        [HttpGet("api/questions/user/{userId}")]
        [Authorize]
        public async Task<IActionResult> Get(int userId)
        {
            var loggedUserId = HttpContext.GetLoggedUserId();
            if (userId != loggedUserId)
                return Unauthorized();

            var questions = await _mediator.Send(new GetQuestionsByUserQuery(loggedUserId));
            return Ok(BaseResponse.Ok(questions));
        }



        [HttpGet("api/questions/{questionId}")]
        public async Task<IActionResult> GetQuestion(int questionId)
        {
            var question = await _mediator.Send(new GetQuestionByIdQuery(questionId));
            return Ok(BaseResponse.Ok(question));
        }

        [HttpPost("api/save/{id:int}")]
        [Authorize]
        public async Task<IActionResult> SaveQuestion(int id)
        {
            var userId = HttpContext.GetLoggedUserId();
            var message = await _mediator.Send(new SaveQuestionCommand(id, userId));
            return Ok(BaseResponse.Ok(message));
        }
        [HttpPost("api/unsave/{questionId:int}")]
        [Authorize]
        public async Task<IActionResult> UnSaveQuestion(int questionId)
        {
            var userId = HttpContext.GetLoggedUserId();
            var command = new UnSaveQuestionCommand { UserId = userId, QuestionId = questionId };
            var response = await _mediator.Send(command);
            return Ok(BaseResponse.Ok(response));
        }

        [HttpPost("api/question/follow/{id:int}")]
        [Authorize]
        public async Task<IActionResult> FollowQuestion(int id)
        {
            var userId = HttpContext.GetLoggedUserId();
            var response = await _mediator.Send(new FollowQuestionCommand { UserId = userId, QuestionId = id });
            return Ok(BaseResponse.Ok(response));
        }

        [HttpPost("api/question/unfollow/{id:int}")]
        [Authorize]
        public async Task<IActionResult> UnFollowQuestion(int id)
        {
            var userId = HttpContext.GetLoggedUserId();
            var response = await _mediator.Send(new UnFollowQuestionCommand
            {
                UserId = userId,
                QuestionId = id
            });
            return Ok(BaseResponse.Ok(response));
        }

        [HttpGet("api/questions/featured")]

        public async Task<IActionResult> GetFeaturedQuestions()
        {
            var command = new GetFeaturedQuestionsQuery();
            var response = await _mediator.Send(command);
            return Ok(BaseResponse.Ok(response));
        }
    }
}
