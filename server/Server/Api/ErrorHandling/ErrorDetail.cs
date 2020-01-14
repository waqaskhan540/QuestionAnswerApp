using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ErrorHandling
{
    public class ErrorDetail
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
