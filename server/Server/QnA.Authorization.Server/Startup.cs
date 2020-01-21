using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QnA.Authorization.Server.Validators;
using QnA.Persistence;
using QnA.Security;
using System;
using System.Linq;

namespace QnA.Authorization.Server
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
            services.AddScoped<IAuthorizationRequestValidator, AuthorizationRequestValidator>();
            services.AddPersistence(Configuration);
            services.AddRazorPages();

            services.AddSecurity(Configuration);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddMvc(config =>
            {
                config.EnableEndpointRouting = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService(typeof(DatabaseContext)) as DatabaseContext;
                    if (!context.DeveloperApps.Any())
                    {
                        var appId = Guid.NewGuid();
                        context.DeveloperApps.Add(new Domain.Entities.DeveloperApp
                        {
                            AppId = appId,
                            AppName = "Test App",
                            UserId = 1,
                            RequiresConsent = true
                        });

                        context.RedirectUrls.Add(new Domain.Entities.RedirectUrl
                        {
                            AppId = appId,
                            RedirectUri = "http://localhost:5000/callback"
                        });

                        context.SaveChanges();
                    }
                }
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
            app.UseMvc();
        }
    }
}
