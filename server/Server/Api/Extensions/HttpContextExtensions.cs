using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.Extensions
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
