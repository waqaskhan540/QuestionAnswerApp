using System;
using System.Collections.Generic;
using System.Text;

namespace QnA.Security.Configuration
{
    public class SecurityOptions
    {
    //     "Secret": "cW5hX2FwcF9zZWN1cml0eV9zZWNyZXQ=",
    //"Issuer": "http://localhost:5000",
    //"Audience": "http://localhost:3000",
    //"Expiry": "24"

        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenExpiry { get; set; }
    }
}
