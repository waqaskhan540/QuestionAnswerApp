using Newtonsoft.Json;

namespace QnA.Authorization.Server.Models
{
    public class AccessTokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }
}
