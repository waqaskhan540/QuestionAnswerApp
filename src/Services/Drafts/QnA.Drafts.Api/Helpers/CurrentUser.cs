using IdentityModel;
using Microsoft.AspNetCore.Http;
using System;

namespace QnA.Drafts.Api.Helpers
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CurrentUser(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public string GetUserId()
        {
            //return _contextAccessor
            //         .HttpContext
            //         .User
            //         .FindFirst(JwtClaimTypes.Subject)?.Value;
            return Guid.NewGuid().ToString(); 
                
            
        }
    }
}
