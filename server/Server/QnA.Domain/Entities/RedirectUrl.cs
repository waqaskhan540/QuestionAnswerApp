using System;

namespace QnA.Domain.Entities
{
    public class RedirectUrl
    {

        public Guid AppId { get; set; }
        public string RedirectUri { get; set; }

        public DeveloperApp App { get; set; }

    }
}
