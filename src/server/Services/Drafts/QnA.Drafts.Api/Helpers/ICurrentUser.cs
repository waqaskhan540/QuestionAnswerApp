using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Drafts.Api.Helpers
{
    public interface ICurrentUser
    {
        public string GetUserId();
    }
}
