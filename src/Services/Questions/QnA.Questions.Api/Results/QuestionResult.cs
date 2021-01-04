using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Questions.Api.Results
{
    public class QuestionResult
    {
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
    }
}
