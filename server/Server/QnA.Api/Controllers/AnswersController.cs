using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QnA.Api.ApiModels;
using QnA.Api.Extensions;
using QnA.Application.Answers.Commands;
using QnA.Application.Answers.Queries;
using System.Threading.Tasks;

namespace QnA.Api.Controllers
{

    public class AnswersController : Controller
    {
        private readonly IMediator _mediator;

        public AnswersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// creates a new answer to a question
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpPost("api/answer")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] AnswerViewModel model)
        {
            var publishCommand = new PublishAnswerCommand
            {
                UserId = HttpContext.GetLoggedUserId(),
                Answer = model.Answer,
                QuestionId = model.QuestionId
            };

            var response = await _mediator.Send(publishCommand);
            return Ok(BaseResponse.Ok(response));

        }
        /// <summary>
        /// gets answers of a given question based on {questionId}
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>

        [HttpGet("api/answers/{questionId:int}")]
        public async Task<IActionResult> GetAnswers(int questionId)
        {
            var answers = await _mediator.Send(new GetAnswersByQuestionIdQuery(questionId));
            return Ok(BaseResponse.Ok(answers));
        }
    }
}
