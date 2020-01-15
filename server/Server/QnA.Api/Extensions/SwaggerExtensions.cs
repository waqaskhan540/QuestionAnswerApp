using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace QnA.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {

            var swaggerConfig = configuration.GetSection("Swagger");

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(swaggerConfig["ApiVersion"], new OpenApiInfo
                {
                    Title = swaggerConfig["Title"],
                    Version = swaggerConfig["ApiVersion"],
                    Description = swaggerConfig["Description"],
                    Contact = new OpenApiContact
                    {
                        Name = swaggerConfig["Contact:Name"],
                        Email = swaggerConfig["Contact:Email"],
                        Url = new System.Uri(swaggerConfig["Contact:Url"])
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public static void UseSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            var swaggerConfig = configuration.GetSection("Swagger");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(swaggerConfig["Endpoint"], swaggerConfig["Title"]);
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
