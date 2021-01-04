using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MediatR;
using QnA.Answers.Api.Data;
using QnA.Answers.Api.Domain;
using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace QnA.Answers.Api
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "QnA.Answers.Api", Version = "v1" });
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

            //database
            string connectionString = Configuration.GetConnectionString("Default");
            services.AddDbContext<AnswersDbContext>(options => options.UseSqlServer(connectionString));

            //repositories
            services.AddTransient<IAnswersRepository, AnswersRepository>();

            //automapper
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Answer, AnswerDto>();                
            });
            var mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);
           
            //validators            
            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);

            //mediatR
            services.AddMediatR(typeof(Startup).Assembly);

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsEnvironment("Local"))
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "QnA.Answers.Api v1"));

                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<AnswersDbContext>();
                    context.Database.EnsureCreated();

                    if (!context.Answers.Any())
                    {
                        var guid = Guid.NewGuid().ToString();

                        context.Answers.Add(new Answer
                        {
                            AuthorId = Guid.NewGuid().ToString(),
                            Description = "this is answer one.",
                            QuestionId = guid,
                        });

                        context.Answers.Add(new Answer
                        {
                            AuthorId = Guid.NewGuid().ToString(),
                            Description = "this is answer two.",
                            QuestionId = guid,
                        });

                        context.Answers.Add(new Answer
                        {
                            AuthorId = Guid.NewGuid().ToString(),
                            Description = "this is answer three.",
                            QuestionId = guid,
                        });

                        context.SaveChanges();
                    }
                }
            }

            app.UseExceptionHandler(options =>
            {
                options.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if(exceptionHandlerFeature?.Error is ValidationException)
                    {
                        ValidationException exception = exceptionHandlerFeature.Error as ValidationException;
                        var errors = exception.Errors.Select(x => x.ErrorMessage);

                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;                        
                        await context.Response.WriteAsync(JsonSerializer.Serialize(errors));
                    }else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var error = new { error = "Something went wrong. please try again." };
                        await context.Response.WriteAsync(JsonSerializer.Serialize(error));
                    }
                });
            });
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
