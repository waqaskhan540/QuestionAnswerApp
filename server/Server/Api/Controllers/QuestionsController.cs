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
                UserId = int.Parse(userId),
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
                                     .Select(q => new
                                     {
                                         q.Id,
                                         q.QuestionText,
                                         q.DateTime,
                                         q.UserId,
                                         User = new
                                         {
                                             q.User.FirstName,
                                             q.User.LastName
                                         }
                                     })
                                     .OrderByDescending(x => x.DateTime)
                                    .ToListAsync();

            return Ok(BaseResponse.Ok(questions));
        }

        [HttpGet("api/questions/user/{userId}")]
        [Authorize]
        public async Task<IActionResult> Get(int userId)
        {
            var loggedUserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            if (userId != int.Parse(loggedUserId))
                return Unauthorized();

            var questions = await _dbContext.Questions
                                        .Where(q => q.UserId == userId)
                                         .Select(q => new
                                         {
                                             q.Id,
                                             q.QuestionText,
                                             q.DateTime,
                                             q.UserId,
                                             User = new
                                             {
                                                 q.User.FirstName,
                                                 q.User.LastName
                                             }
                                         })
                                         .OrderByDescending(x => x.DateTime)
                                        .ToListAsync();
            return Ok(BaseResponse.Ok(questions));
        }

        [HttpGet("api/questions/user/{userId}/question/{questionId}")]
        public async Task<IActionResult> Get(int userId, int questionId)
        {
            var question = await _dbContext.Questions
                                .FirstOrDefaultAsync(x => x.UserId == userId && x.Id == questionId);
            return Ok(BaseResponse.Ok(question));
        }

        [HttpGet("api/questions/{questionId}")]
        public async Task<IActionResult> GetQuestion(int questionId)
        {
            var question = await _dbContext.Questions
                                     .Select(q => new
                                     {
                                         q.Id,
                                         q.QuestionText,
                                         q.DateTime,
                                         q.UserId,
                                         User = new
                                         {
                                             q.User.FirstName,
                                             q.User.LastName
                                         }
                                     })
                                     .Where(q => q.Id == questionId)
                                    .FirstOrDefaultAsync();
            return Ok(BaseResponse.Ok(question));
        }

        [HttpPost("api/save/{id:int}")]
        [Authorize]
        public async Task<IActionResult> SaveQuestion(int id)
        {
            var question = await _dbContext.Questions.FirstOrDefaultAsync(x => x.Id == id);
            if(null == question)
            {
                return BadRequest();
            }

            var loggedUserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var existing = await _dbContext.SavedQuestions.FirstOrDefaultAsync(x => x.QuestionId == id && x.UserId == int.Parse(loggedUserId));

            if (null != existing)
                return Ok();

            var savedQuestion = new SavedQuestion
            {
                UserId = int.Parse(loggedUserId),
                QuestionId = id,
                DateTime = DateTime.Now
            };

            await _dbContext.SavedQuestions.AddAsync(savedQuestion);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

    }
}
