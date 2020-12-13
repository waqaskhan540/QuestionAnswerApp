using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace QnA.Api.Extensions
{
    public static class HttpContextExtensions
    {
        public static int GetLoggedUserId(this HttpContext context)
        {
            var id = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return Convert.ToInt32(id);
        }
    }
}
