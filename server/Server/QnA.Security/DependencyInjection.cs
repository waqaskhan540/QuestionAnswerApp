using Microsoft.Extensions.DependencyInjection;
using QnA.Application.Interfaces.Security;

namespace QnA.Security
{
    public static class DependencyInjection
    {
        public static void AddSecurity(this IServiceCollection services)
        {
            services.AddScoped<IHashGenerator, HashGenerator>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        }
    }
}
