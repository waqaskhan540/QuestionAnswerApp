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
    
    public class QuestionsController : Controller
    {
        private readonly DatabaseContext _dbContext;
        public QuestionsController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpPost("api/questions")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody]QuestionViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var question = new Question
            {
                DateTime = DateTime.Now,
                UserId =  int.Parse(userId),
                QuestionText = model.QuestionText
            };

            await _dbContext.Questions.AddAsync(question);
            await _dbContext.SaveChangesAsync();

            return Ok(BaseResponse.Ok(new { question.Id }));
        }

        [HttpGet("api/questions")]
        public async Task<IActionResult> GetAll()
        {
            var questions = await _dbContext.Questions
                                     .Select(q => new {
                                         q.Id,
                                         q.QuestionText,
                                         q.DateTime,
                                         q.UserId,
                                         User = new  {
                                             q.User.FirstName,
                                             q.User.LastName
                                         }
                                     })                                    
                                    .ToListAsync();

            return Ok(BaseResponse.Ok(questions));
        }

        [HttpGet("api/questions/user/{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            var questions = await _dbContext.Questions.Where(q => q.UserId == userId).ToListAsync();
            return Ok(BaseResponse.Ok(questions));
        }

        [HttpGet("api/questions/user/{userId}/question/{questionId}")]
        public async Task<IActionResult> Get(int userId,int questionId)
        {
            var question = await _dbContext.Questions
                                .FirstOrDefaultAsync(x => x.UserId == userId && x.Id == questionId);
            return Ok(BaseResponse.Ok(question));
        }

        [HttpGet("api/questions/{questionId}")]
        public async Task<IActionResult> GetQuestion(int questionId)
        {
            var question = await _dbContext.Questions.FirstOrDefaultAsync(x => x.Id == questionId);
            return Ok(BaseResponse.Ok(question));
        }

        [HttpGet("api/myquestions")]
        [Authorize]
        public async Task<IActionResult> GetMyQuestions()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var questions = await _dbContext.Questions.Where(x => x.Id == int.Parse(userId)).ToListAsync();
            return Ok(BaseResponse.Ok(questions));
        }
    }
}
