using Api.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QnA.Application;
using QnA.Authentication;
using QnA.Persistence;


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

            services.AddCors(config =>
            {
                config.AddPolicy("AllowAll", options =>
                 {
                     options.AllowAnyHeader();
                     options.AllowAnyMethod();
                     options.AllowAnyOrigin();
                 });
            });


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
            }

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();
                context.Database.EnsureCreated();
            }

            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
