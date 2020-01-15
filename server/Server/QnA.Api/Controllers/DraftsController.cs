using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QnA.Api.ApiModels;
using QnA.Api.Extensions;
using QnA.Application.Drafts.Commands;
using QnA.Application.Drafts.Queries;
using QnA.Persistence;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    public class DraftsController : Controller
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMediator _mediator;

        public DraftsController(DatabaseContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        /// <summary>
        /// creates a new draft for an unpublished answer
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("api/drafts")]
        public async Task<IActionResult> SaveDraft([FromBody]DraftViewModel model)
        {

            var command = new SaveDraftCommand
            {
                UserId = HttpContext.GetLoggedUserId(),
                Content = model.Content,
                QuestionId = model.QuestionId
            };
            var response = await _mediator.Send(command);
            return Ok(BaseResponse.Ok(response.Message));
        }

        /// <summary>
        /// gets the drafts for current user
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/drafts")]
        public async Task<IActionResult> GetDrafts()
        {
            var userId = HttpContext.GetLoggedUserId();
            var drafts = await _mediator.Send(new GetDraftsQuery(userId));
            return Ok(BaseResponse.Ok(drafts));
        }

        /// <summary>
        /// gets the number of drafts the current user has
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/drafts/count")]
        public async Task<IActionResult> GetDraftsCount()
        {
            var userId = HttpContext.GetLoggedUserId();
            var draftCount = await _mediator.Send(new GetDraftsCountQuery(userId));
            return Ok(BaseResponse.Ok(new { draftCount }));
        }


    }
}
