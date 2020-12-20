using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QnA.Questions.Api.Domain;
using QnA.Questions.Api.Models;
using System;
using System.Threading.Tasks;

namespace QnA.Questions.Api.Controllers
{
    [ApiController]
    [Route("api/q")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionsService _questionService;

        public QuestionsController(IQuestionsService questionService)
        {
            _questionService = questionService;
        }

        [AllowAnonymous]
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Content("questions api working.");
        }

        /// <summary>
        /// get a question by its Id
        /// </summary>
        /// <param name="questionId">Id of question (GUID)</param>
        /// <returns></returns>
        [HttpGet("{questionId}")]
        public async Task<IActionResult> GetQuestionById(string questionId)
        {
            var question = await _questionService.GetById(Guid.Parse(questionId));
            if(question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }
        /// <summary>
        /// get question by author id
        /// </summary>
        /// <param name="authorId"></param>
        /// <returns></returns>

        [HttpGet("author/{authorId}")]
        public async Task<IActionResult> GetQuestionsByAuthorId(string authorId)
        {
            var questions = await _questionService.GetByAuthorId(authorId);
            return Ok(questions);
        }

        /// <summary>
        /// Create a new question entry
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionModel model)
        {
            var result = await _questionService.Create(model);
            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result);
        }

        [HttpDelete("{questionId}")]
        public async Task<IActionResult> DeleteQuestion(string questionId)
        {
            var result = await _questionService.Delete(Guid.Parse(questionId));
            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result);
        }
    }
}
