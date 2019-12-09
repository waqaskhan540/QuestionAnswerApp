using Api.ApiModels;
using Api.Data;
using Api.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.Controllers
{

    public class AnswersController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public AnswersController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("api/answer")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] AnswerViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var questionExists = _dbContext.Questions.Any(x => x.Id == model.QuestionId);
            if (!questionExists)
                return BadRequest(BaseResponse.Error("question does not exist."));

            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var answer = new Answer
            {
                AnswerMarkup = model.Answer,
                DateTime = DateTime.Now,
                questionId = model.QuestionId,
                UserId = int.Parse(userId)
            };

            await _dbContext.Answers.AddAsync(answer);            
            await _dbContext.SaveChangesAsync();

            return Ok(BaseResponse.Ok("Answered published successfully."));

        }

        [HttpGet("api/answers/{questionId:int}")]
        public async Task<IActionResult> GetAnswers(int questionId)
        {

            var answers = await _dbContext.Answers
                                .Where(x => x.questionId == questionId)
                                .Include(q => q.User)
                                //.Include(q => q.Question)
                                //    .ThenInclude(q => q.User)
                                .Select(a => new
                                {
                                    a.AnswerId,
                                    a.AnswerMarkup,
                                    a.DateTime,
                                    user = new
                                    {
                                        a.User.FirstName,
                                        a.User.LastName,
                                        a.User.Email

                                    }
                                })
                                .ToListAsync();

            return Ok(BaseResponse.Ok(answers));
        }
    }
}
