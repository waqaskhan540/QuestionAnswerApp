using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using QnA.Application.Interfaces;
using System.Text;

namespace QnA.Authentication
{
    public static class DependencyInjection
    {
        public static void AddAuthencticationServices(this IServiceCollection services)
        {
            services.AddScoped<IExternalAuthenticationProvider, ExternalAuthenticationProvider>();

            /** Security **/
            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("some_big_key_value_here_secret")),
                    ValidateIssuer = false,
                    ValidateAudience = false

                };
            });
        }
    }
}
