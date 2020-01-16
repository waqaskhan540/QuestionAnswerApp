using System;

namespace QnA.Authorization.Server.Entities
{
    public class RedirectUrls
    {
        public Guid ClientId { get; set; }
        public string RedirectUrl { get; set; }

        public Client Client { get; set; }
    }
}
