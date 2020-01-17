using Microsoft.AspNetCore.Http;
using QnA.Authorization.Server.Models;

namespace QnA.Authorization.Server.Extensions
{
    public static class QueryCollectionExtensions
    {
        public static AuthorizationRequest AsAuthorizationRequest(this IQueryCollection query)
        {
            var request = new AuthorizationRequest();

            request.ClientId = query.ContainsKey("client_id") ? query["client_id"].ToString() : string.Empty;
            request.RedirectUri = query.ContainsKey("redirect_uri") ? query["redirect_uri"].ToString() : string.Empty;
            request.ResponseType = query.ContainsKey("response_type") ? query["response_type"].ToString() : string.Empty;
            request.Scope = query.ContainsKey("scope") ? query["scope"].ToString() : string.Empty;
            request.State = query.ContainsKey("state") ? query["state"].ToString() : string.Empty;

            return request;
        }
    }
}
