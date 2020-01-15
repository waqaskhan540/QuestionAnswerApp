using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QnA.Api.ApiModels;
using System.Linq;
using System.Threading.Tasks;

namespace QnA.Api.Filters
{
    public class ModelStateFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ModelState.IsValid)
                await next();

            var errors = context.ModelState.Values.SelectMany(x => x.Errors)
                            .Select(x => x.ErrorMessage)
                            .Where(x => !string.IsNullOrEmpty(x))
                            .ToList();
            string message = "The input parameters are invalid.";

            context.Result = new BadRequestObjectResult(BaseResponse.Error(message, errors));
        }
    }
}
