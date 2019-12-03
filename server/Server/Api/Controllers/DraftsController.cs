using Api.ApiModels;
using Api.Data;
using Api.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    public class DraftsController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public DraftsController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("api/draft")]
        public async Task<IActionResult> SaveDraft([FromBody]DraftViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var loggedUserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var draft = _dbContext.Drafts.FirstOrDefault(x => x.QuestionId == model.QuestionId && x.UserId == int.Parse(loggedUserId));

            if (null != draft)
            {
                draft.Content = model.Content;
                draft.DateTime = DateTime.Now;
                _dbContext.Drafts.Update(draft);
            }
            else
            {
                var newDraft = new Draft
                {
                    UserId = int.Parse(loggedUserId),
                    DateTime = DateTime.Now,
                    Content = model.Content,
                    QuestionId = model.QuestionId
                };
                await _dbContext.Drafts.AddAsync(newDraft);
            }
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
