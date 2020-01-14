using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using QnA.Application.Interfaces;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Authentication
{
    public static class DependencyInjection
    {
        public static void AddAuthencticationServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IExternalAuthenticationProvider, ExternalAuthenticationProvider>();

            /** Security **/
            string secret = configuration["Security:Secret"];
            var key = Encoding.UTF8.GetBytes(secret);
            var signingKey = new SymmetricSecurityKey(key);

            string issuer = configuration.GetSection("Security")["Issuer"];
            string audience = configuration.GetSection("Security")["Audience"];

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
                    IssuerSigningKey = signingKey,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidIssuer = issuer                    
                };
                config.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/followings"))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                    
                };
            });
        }
    }
}
