using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QnA.Answers.Api.Domain;
using QnA.Answers.Api.Models;
using System;
using System.Threading.Tasks;

namespace QnA.Answers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AnswersController : ControllerBase
    {
        private readonly IAnswersService _answersService;

        public AnswersController(IAnswersService answersService)
        {
            _answersService = answersService;
        }

        /// <summary>
        /// Get answer by its Id.
        /// </summary>
        /// <param name="answerId"></param>
        /// <returns></returns>
        [HttpGet("{answerId}")]
        public async Task<IActionResult> GetAnswerById(string answerId)
        {
            var answer = await _answersService.GetById(Guid.Parse(answerId));
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
            var answers = await _answersService.GetByQuestionId(Guid.Parse(questionId));
            return Ok(answers);
        }

        /// <summary>
        /// Create a new answer entry
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateAnswerModel model)
        {
            var result = await _answersService.Create(model);
            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }
    }
}
