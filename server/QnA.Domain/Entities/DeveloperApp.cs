using System;
using System.Collections.Generic;

namespace QnA.Domain.Entities
{
    public class DeveloperApp
    {
        public Guid AppId { get; set; }
        public string AppName { get; set; }
        public bool RequiresConsent { get; set; }

        public int UserId { get; set; }
        public AppUser Developer { get; set; }

        public ICollection<RedirectUrl> RedirectUrls { get; set; }
    }
}
