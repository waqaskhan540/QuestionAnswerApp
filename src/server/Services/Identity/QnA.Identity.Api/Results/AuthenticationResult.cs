using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Identity.Api.Results
{
    public class AuthenticationResult
    {
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }

        public JwtTokenResult TokenResult { get; set; }
    }
}
