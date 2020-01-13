using Newtonsoft.Json.Linq;
using QnA.Application.Interfaces;
using QnA.Application.Interfaces.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace QnA.Authentication
{
    public class ExternalAuthenticationProvider : IExternalAuthenticationProvider
    {
        public async Task<ExternalLoginResult> LoginExternal(string provider, string accessToken)
        {
            switch (provider.ToLower())
            {
                case "facebook":
                    return await new FacebookAuthenticationProvider().GetUserInfo(accessToken);
                case "google":
                    return await new GoogleAuthenticationProvider().GetUserInfo(accessToken);
                default:
                    return new ExternalLoginResult { Error = "Invalid Provider" };
            }
        }
    }

    public class FacebookAuthenticationProvider
    {
        public async Task<ExternalLoginResult> GetUserInfo(string accessToken)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"https://graph.facebook.com/v5.0/me?access_token={accessToken}&fields=id,first_name,last_name,email,picture");
                var result = JToken.Parse(await response.Content.ReadAsStringAsync());

                var firstname = result["first_name"].ToString();
                var lastname = result["last_name"].ToString();
                var email = result["email"].ToString();
                var picture = result["picture"]["data"]["url"].ToString();

                return new ExternalLoginResult
                {
                    FirstName = firstname,
                    LastName = lastname,
                    Email = email,
                    Picture = picture
                };
            }
        }
    }

    public class GoogleAuthenticationProvider
    {
        public async Task<ExternalLoginResult> GetUserInfo(string accessToken)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"https://www.googleapis.com/oauth2/v2/userinfo?access_token={accessToken}");
                var result = JToken.Parse(await response.Content.ReadAsStringAsync());

                var firstname = result["given_name"].ToString();
                var lastname = result["family_name"].ToString();
                var email = result["email"].ToString();
                var picture = result["picture"].ToString();

                return new ExternalLoginResult
                {
                    FirstName = firstname,
                    LastName = lastname,
                    Email = email,
                    Picture = picture
                };
            }
        }
    }
}
