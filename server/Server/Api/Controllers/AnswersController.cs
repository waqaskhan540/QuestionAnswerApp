using Api.ApiModels;
using Api.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QnA.Application.Answers.Commands;
using QnA.Application.Answers.Queries;
using QnA.Domain.Entities;
using QnA.Persistence;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Controllers
{

    public class AnswersController : Controller
    {        
        private readonly IMediator _mediator;

        public AnswersController(IMediator mediator)
        {           
            _mediator = mediator;
        }

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

        [HttpGet("api/answers/{questionId:int}")]
        public async Task<IActionResult> GetAnswers(int questionId)
        {
            var answers = await _mediator.Send(new GetAnswersByQuestionIdQuery(questionId));
            return Ok(BaseResponse.Ok(answers));
        }
    }
}
