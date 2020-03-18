using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QnA.Application.Interfaces;
using QnA.Application.Interfaces.Repositories;
using QnA.Persistence.Repositories;

namespace QnA.Persistence
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            /** Databases **/
            string connectionString = configuration.GetConnectionString("Default");
            services.AddDbContext<DatabaseContext>(
                options => options.UseSqlServer(connectionString,
                b => b.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName)));

            services.AddScoped<IDatabaseContext>(provider => provider.GetService<DatabaseContext>());

            /** repositories **/

            services.AddScoped<IQuestionsRepository, QuestionsRepository>();
            services.AddScoped<IAnswersRepository, AnswersRepository>();
            services.AddScoped<IQuestionsFollowingRepository, QuestionsFollowingRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }
    }
}
