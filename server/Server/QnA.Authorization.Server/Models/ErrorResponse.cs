using Newtonsoft.Json;

namespace QnA.Authorization.Server.Models
{
    public class ErrorResponse
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }

        [JsonProperty("error_uri")]
        public string ErrorUri { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }
}
