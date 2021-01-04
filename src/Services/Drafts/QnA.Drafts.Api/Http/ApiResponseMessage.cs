using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Drafts.Api.Http
{
    public class ApiResponseMessage<T> : IActionResult
    {
        private readonly ICustomHttpResponse<T> _httpResponse;
        public ApiResponseMessage(ICustomHttpResponse<T> httpResponse)
        {
            _httpResponse = httpResponse;
        }
        public async Task ExecuteResultAsync(ActionContext context)
        {
            switch (_httpResponse.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                     await new OkObjectResult(_httpResponse).ExecuteResultAsync(context);
                    break;
                case System.Net.HttpStatusCode.BadRequest:
                    await new BadRequestObjectResult(_httpResponse).ExecuteResultAsync(context);
                    break;
                case System.Net.HttpStatusCode.NotFound:
                    await new NotFoundObjectResult(_httpResponse).ExecuteResultAsync(context);
                    break;
                    
            }
        }
    }

    public class ApiResponseMessage : IActionResult
    {
        private readonly ICustomHttpResponse _httpResponse;
        public ApiResponseMessage(ICustomHttpResponse httpResponse)
        {
            _httpResponse = httpResponse;
        }
        public async Task ExecuteResultAsync(ActionContext context)
        {
            switch (_httpResponse.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    await new OkObjectResult(_httpResponse).ExecuteResultAsync(context);
                    break;
                case System.Net.HttpStatusCode.BadRequest:
                    await new BadRequestObjectResult(_httpResponse).ExecuteResultAsync(context);
                    break;
                case System.Net.HttpStatusCode.NotFound:
                    await new NotFoundObjectResult(_httpResponse).ExecuteResultAsync(context);
                    break;
                case System.Net.HttpStatusCode.Created:
                    await new CreatedResult(_httpResponse.Path, _httpResponse).ExecuteResultAsync(context);
                    break;
                
            }
        }
    }
}
