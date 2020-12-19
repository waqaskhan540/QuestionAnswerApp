using Microsoft.AspNetCore.Mvc;
using QnA.Drafts.Api.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Drafts.Api.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult HttpResponse<T>(ICustomHttpResponse<T> httpResponse) 
        {
            return new ApiResponseMessage<T>(httpResponse);
        }

        protected IActionResult HttpResponse(ICustomHttpResponse httpResponse)
        {
            return new ApiResponseMessage(httpResponse);
        }
    }
}
