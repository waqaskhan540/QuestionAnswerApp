using Newtonsoft.Json;

namespace QnA.Authorization.Server.Models
{
    public class AccessTokenRequest
    {
        [JsonProperty("grant_type")]
        public string GrantType { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("redirect_uri")]
        public string RedirectUri { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }
    }
}
