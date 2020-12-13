using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QnA.Api.ApiModels;
using QnA.Api.Extensions;
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


        /// <summary>
        /// creates a new question on behalf of current user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("api/questions")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody]QuestionViewModel model)
        {
            var userId = HttpContext.GetLoggedUserId();
            var questionId = await _mediator.Send(new AddQuestionCommand(userId, model.QuestionText));

            return await GetQuestion(questionId);
        }

        /// <summary>
        /// gets user's feed data
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet("api/feed/{page:int}")]
        public async Task<IActionResult> GetFeed(int page = 1)
        {
            var questions = await _mediator.Send(new GetFeedByPageQuery(page: page));
            return Ok(BaseResponse.Ok(questions));
        }

        /// <summary>
        /// gets questions posted by current user
        /// </summary>        
        /// <returns></returns>
        [HttpGet("api/questions/user/")]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var loggedUserId = HttpContext.GetLoggedUserId();
            var questions = await _mediator.Send(new GetQuestionsByUserQuery(loggedUserId));
            return Ok(BaseResponse.Ok(questions));
        }


        /// <summary>
        /// gets a question by {questionId}
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        [HttpGet("api/questions/{questionId}")]
        public async Task<IActionResult> GetQuestion(int questionId)
        {
            var question = await _mediator.Send(new GetQuestionByIdQuery(questionId));
            return Ok(BaseResponse.Ok(question));
        }

        /// <summary>
        /// marks the question as saved.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("api/save/{id:int}")]
        [Authorize]
        public async Task<IActionResult> SaveQuestion(int id)
        {
            var userId = HttpContext.GetLoggedUserId();
            var message = await _mediator.Send(new SaveQuestionCommand(id, userId));
            return Ok(BaseResponse.Ok(message));
        }

        /// <summary>
        /// unsaves a questions
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        [HttpPost("api/unsave/{questionId:int}")]
        [Authorize]
        public async Task<IActionResult> UnSaveQuestion(int questionId)
        {
            var userId = HttpContext.GetLoggedUserId();
            var command = new UnSaveQuestionCommand { UserId = userId, QuestionId = questionId };
            var response = await _mediator.Send(command);
            return Ok(BaseResponse.Ok(response));
        }

        /// <summary>
        /// follow a question
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("api/question/follow/{id:int}")]
        [Authorize]
        public async Task<IActionResult> FollowQuestion(int id)
        {
            var userId = HttpContext.GetLoggedUserId();
            var response = await _mediator.Send(new FollowQuestionCommand { UserId = userId, QuestionId = id });
            return Ok(BaseResponse.Ok(response));
        }

        /// <summary>
        /// unfollow a question
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// gets featured questions
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/questions/featured")]
        public async Task<IActionResult> GetFeaturedQuestions()
        {
            var command = new GetFeaturedQuestionsQuery();
            var response = await _mediator.Send(command);
            return Ok(BaseResponse.Ok(response));
        }
    }
}
