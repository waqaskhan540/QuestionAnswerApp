using Api.ErrorHandling;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder appBuilder,ILoggerFactory loggerFactory)
        {
            appBuilder.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if(contextFeature != null)
                    {
                        var _logger = loggerFactory.CreateLogger(typeof(Startup));
                        var error = new ErrorDetail
                        {
                            ErrorMessage = "Internal Server Error",
                            StatusCode = context.Response.StatusCode
                        }.ToString();

                        _logger.LogError($"Exception:{contextFeature.Error}");
                        await context.Response.WriteAsync(error);                  
                    }
                });
            });
        }
    }

}
