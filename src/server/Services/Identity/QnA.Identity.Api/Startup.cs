using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using QnA.Identity.Api.Data;
using QnA.Identity.Api.Domain;
using QnA.Identity.Api.Helpers;
using QnA.Identity.Api.Models;
using QnA.Identity.Api.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QnA.Identity.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "QnA.Identity.Api", Version = "v1" });
            });

            //database
            string connectionString = Configuration.GetConnectionString("Default");
            services.AddDbContext<UsersDbContext>(options => options.UseSqlServer(connectionString,sqlServerOptionsAction => 
            {
                sqlServerOptionsAction.EnableRetryOnFailure(3);
            }));

            services.AddIdentity<ApplicationUser,IdentityRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<UsersDbContext>();

            //services
            services.AddTransient<IAuthService, AuthService>();

            //validators
            services.AddTransient<IValidator<LoginUserModel>, LoginUserModelValidator>();
            services.AddTransient<IValidator<RegisterUserModel>, RegisterUserModelValidator>();

            //helpers
            services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();
                
                
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "QnA.Identity.Api v1"));

                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
                    context.Database.EnsureCreated();
                }
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
