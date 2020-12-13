using Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QnA.Api.Extensions;
using QnA.Api.Filters;
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
            services.AddAuthencticationServices(Configuration);
            services.AddSecurity(Configuration);
            services.AddStorage();
            services.AddSignalR();

            string clientAppUrl = Configuration["ClientAppUrl"];
            services.AddCors(config =>
            {
                config.AddPolicy("AllowAll", options =>
                 {
                     options.AllowAnyHeader()
                             .AllowAnyMethod()
                             .WithOrigins(clientAppUrl)
                             .AllowCredentials();
                 });
            });


            services.AddHttpContextAccessor();
            services.AddControllers();

            services.AddSwagger(Configuration);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //if (env)
           // {
                app.UseDeveloperExceptionPage();
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();
                    context.Database.EnsureCreated();
                    context.SeedData();
                }
            //}


            app.ConfigureExceptionHandler(loggerFactory);
            app.UseStaticFiles();
            app.UseCors("AllowAll");
            
            app.UseRouting();
            app.UseAuthentication();

            //app.UseSignalR(routes =>
            //{
            //    routes.MapHub<FollowHub>("/followings");
            //});

            app.UseSwagger(Configuration);
            app.UseEndpoints(endpoints => endpoints.MapControllers());

        }
    }
}
