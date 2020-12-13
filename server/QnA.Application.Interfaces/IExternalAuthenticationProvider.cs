
using QnA.Common.Models;
using System.Threading.Tasks;

namespace QnA.Application.Interfaces
{
    public interface IExternalAuthenticationProvider
    {
        Task<ExternalLoginResult> LoginExternal(string provider, string accessToken);
    }
}
