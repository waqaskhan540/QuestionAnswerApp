using Api.ApiModels;
using Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet("api/questions")]
        public async Task<IActionResult> GetAll()
        {
            var questions = await _dbContext.Questions.ToListAsync();
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
    }
}
