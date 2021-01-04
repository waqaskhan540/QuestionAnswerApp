using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QnA.Drafts.Api.Domain;
using QnA.Drafts.Api.Http;
using QnA.Drafts.Api.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace QnA.Drafts.Api.Controllers
{
    [Route("api/drafts")]
    [ApiController]
   // [Authorize]
    public class DraftsController : BaseController
    {
        private readonly IDraftService _draftService;

        public DraftsController(IDraftService draftService)
        {
            _draftService = draftService;
        }

        /// <summary>
        /// create a draft
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]        
        public async Task<IActionResult> Create([FromBody]CreateDraftModel model)
        {
            var response = await _draftService.Create(model);
            return HttpResponse(response);
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="draftId"></param>
        /// <returns></returns>
        [HttpGet("{draftId}")]
        public async Task<IActionResult> GetById(string draftId)
        {
            var response = await _draftService.GetById(Guid.Parse(draftId));
            return HttpResponse(response);           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorId"></param>
        /// <returns></returns>

        [HttpGet("author/{authorId}")]
        public async Task<IActionResult> GetByAuthorId(string authorId)
        {
            var response = await _draftService.GetByAuthorId(authorId);
            return HttpResponse(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateDraftModel model)
        {
            var response = await _draftService.Update(model);
            return HttpResponse(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="draftId"></param>
        /// <returns></returns>
        [HttpDelete("{draftId}")]
        public async Task<IActionResult> Delete(string draftId)
        {
            var response = await _draftService.Delete(Guid.Parse(draftId));
            return HttpResponse(response);
        }

        
    }
}
