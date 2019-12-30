using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using QnA.Application.Interfaces;
using System.Text;

namespace QnA.Persistence
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            /** Databases **/
            string connectionString = configuration.GetConnectionString("Default");
            services.AddDbContext<DatabaseContext>(
                options => options.UseMySql(connectionString,
                b => b.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName)));

            services.AddScoped<IDatabaseContext>(provider => provider.GetService<DatabaseContext>());


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
