using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Questions.Api.Helpers
{
    public interface ICurrentUser
    {
        string GetUserId();
    }
}
