using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Drafts.Api.Results
{
    public class DraftResult
    {
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
    }
}
