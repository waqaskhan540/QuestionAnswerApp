using System;
using System.Collections.Generic;
using System.Text;

namespace QnA.Application.Exceptions
{
    public class InvalidQuestionException:Exception
    {
        public InvalidQuestionException():base("Invalid question")
        {

        }
    }
}
