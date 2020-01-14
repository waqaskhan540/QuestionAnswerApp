﻿using Api.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QnA.Application;
using QnA.Authentication;
using QnA.FileStorage;
using QnA.Persistence;
using QnA.RealTime.Hubs;
using QnA.Security;

namespace Api
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

            services.AddApplication();
            services.AddPersistence(Configuration);
            services.AddAuthencticationServices();
            services.AddSecurity();
            services.AddStorage();
            services.AddSignalR();

            services.AddCors(config =>
            {
                config.AddPolicy("AllowAll", options =>
                 {
                     options.AllowAnyHeader()
                             .AllowAnyMethod()
                             .WithOrigins("http://localhost:3000")
                             .AllowCredentials();
                 });
            });


            services.AddHttpContextAccessor();
            services.AddMvc(config =>
            {
                config.Filters.Add(new ModelStateFilter());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();
                    context.Database.EnsureCreated();
                    context.SeedData();


                }
            }



            app.UseStaticFiles();
            app.UseCors("AllowAll");
            app.UseAuthentication();

            app.UseSignalR(routes =>
            {
                routes.MapHub<FollowHub>("/followings");
            });
            app.UseMvc();

        }
    }
}
