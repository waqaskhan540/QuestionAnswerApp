
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QnA.Application.Interfaces.Security;
using QnA.Security.Configuration;

namespace QnA.Security
{
    public static class DependencyInjection
    {
        public static void AddSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IHashGenerator, HashGenerator>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped(typeof(IPublicApiAccessTokenGenerator<TokenResult>), typeof(PublicAPIAccessTokenGenerator));
            services.Configure<SecurityOptions>(configuration.GetSection("Security"));

        }
    }
}
