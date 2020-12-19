using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Identity.Api.Results
{
    public class JwtTokenResult
    {        
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
