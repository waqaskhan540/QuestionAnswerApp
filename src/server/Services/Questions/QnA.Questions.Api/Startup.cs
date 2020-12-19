using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QnA.Questions.Api.Data;
using QnA.Questions.Api.Domain;
using QnA.Questions.Api.Helpers;
using QnA.Questions.Api.Models;
using QnA.Questions.Api.Validators;

namespace QnA.Questions.Api
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

            

            //data
            string connectionString = Configuration.GetConnectionString("Default");
            services.AddDbContext<QuestionsContext>(options => options.UseSqlServer(connectionString));

            services.AddTransient<IQuestionsRepository, QuestionsRepository>();

            //automapper
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Question, QuestionDto>();
                cfg.CreateMap<CreateQuestionModel, Question>();
            });
            IMapper mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);

            //services
            services.AddTransient<IQuestionsService, QuestionsService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "QnA.Questions.Api", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Jwt Bearer authentication"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
            });

            //validators
            services.AddTransient<IValidator<CreateQuestionModel>, CreateQuestionModelValidator>();
            
            //authentication
            string secret = Configuration["Auth:Secret"];
            string issuer = Configuration["Auth:Issuer"];
            string audience = Configuration["Auth:Audience"];
            
            var issuerSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = issuerSigninKey,
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidAudience = audience                
            };

            services.AddAuthentication(config => {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(config =>
                {
                    config.TokenValidationParameters = tokenValidationParameters;
                });

            //http
            services.AddHttpContextAccessor();
            services.AddTransient<ICurrentUser, CurrentUser>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "QnA.Questions.Api v1"));

                using(var scope = app.ApplicationServices.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<QuestionsContext>();
                    context.Database.EnsureCreated();

                    if (!context.Questions.Any())
                    {
                        context.Questions.Add(new Question
                        {
                            Id = Guid.NewGuid(),
                            AuthorId = Guid.NewGuid().ToString(),
                            Title = "What is the capital of Pakistan?",
                            PublishedDate = DateTime.UtcNow,
                            ModifiedDate = DateTime.UtcNow
                        }) ;

                        context.Questions.Add(new Question
                        {
                            Id = Guid.NewGuid(),
                            AuthorId = Guid.NewGuid().ToString(),
                            Title = "What is the capital of Pakistan?",
                            PublishedDate = DateTime.UtcNow,
                            ModifiedDate = DateTime.UtcNow
                        });

                        context.SaveChanges();
                    }
                }
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
