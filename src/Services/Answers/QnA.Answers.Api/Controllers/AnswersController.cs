using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QnA.Answers.Api.Commands;
using QnA.Answers.Api.Queries;
using System;
using System.Threading.Tasks;

namespace QnA.Answers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AnswersController : ControllerBase
    {
       
        private readonly IMediator _mediator;

        public AnswersController(IMediator mediator)
        {            
            _mediator = mediator;
        }

        /// <summary>
        /// Get answer by its Id.
        /// </summary>
        /// <param name="answerId"></param>
        /// <returns></returns>
        [HttpGet("{answerId}")]
        public async Task<IActionResult> GetAnswerById(string answerId)
        {
            var answer = await _mediator.Send(new GetAnswerByIdQuery
            {
                AnswerId = answerId
            });

            if (answer == null)
                return NotFound();

            return Ok(answer);
        }

        /// <summary>
        /// Get answers by question Id
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        [HttpGet("q/{questionId}")]
        public async Task<IActionResult> GetAnswersByQuestionId(string questionId)
        {
            var answers = await _mediator.Send(new GetAnswersByQuestionIdQuery
            {
                QuestionId = questionId
            });
            return Ok(answers);
        }

        /// <summary>
        /// Create a new answer entry
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAnswerCommand model)
        {
            var answer = await _mediator.Send(model);
            return Ok(answer);
            
        }
    }
}
