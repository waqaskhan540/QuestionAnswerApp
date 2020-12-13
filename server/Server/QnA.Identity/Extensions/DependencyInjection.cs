using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QnA.Application.Interfaces.Authentication;
using QnA.Identity.Entities;
using QnA.Identity.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Identity.Extensions
{
    public static class DependencyInjection
    {
        public static void AddQnAIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            //string connectionString = configuration.GetConnectionString("Default");
            //services.AddDbContext<QnAIdentityContext>(
            //    options => options.UseSqlServer(connectionString,
            //    b => b.MigrationsAssembly(typeof(QnAIdentityContext).Assembly.FullName)));

            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddDefaultTokenProviders()
            //    .AddEntityFrameworkStores<QnAIdentityContext>();


            AddServices(services);
        }

        private static void AddServices(IServiceCollection services)
        {

            services.AddTransient<IUserService, UserService>();
        }
    }
}
