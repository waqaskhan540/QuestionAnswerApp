using Newtonsoft.Json;

namespace QnA.Authorization.Server.Models
{
    public class AuthorizationResponse
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }
}
