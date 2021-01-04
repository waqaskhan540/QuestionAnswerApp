using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QnA.Drafts.Api.Data;
using QnA.Drafts.Api.Domain;
using QnA.Drafts.Api.Helpers;
using QnA.Drafts.Api.Models;
using QnA.Drafts.Api.Validators;
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;

namespace QnA.Drafts.Api
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "QnA.Drafts.Api", Version = "v1" });
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
            services.AddDbContext<DraftsContext>(
                options => options.UseSqlServer(connectionString, 
                        sqlOptions => { sqlOptions.EnableRetryOnFailure(3); }));

            //repositories
            services.AddTransient<IDraftRepository, DraftRepository>();

            //services
            services.AddTransient<IDraftService, DraftsService>();

            //validators
            services.AddTransient<IValidator<CreateDraftModel>, CreateDraftModelValidator>();
            services.AddTransient<IValidator<UpdateDraftModel>, UpdateDraftModelValidator>();
            

            //automapper
            var configuration = new MapperConfiguration(config =>
            {
                config.CreateMap<CreateDraftModel, Draft>();
                config.CreateMap<Draft, DraftDto>();
            });
            var mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);

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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "QnA.Drafts.Api v1"));

                using(var scope = app.ApplicationServices.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<DraftsContext>();
                    context.Database.EnsureCreated();

                    if (!context.Drafts.Any())
                    {
                        context.Drafts.Add(new Draft
                        {
                            Content = "Test draft",
                            QuestionId = Guid.NewGuid(),
                            AuthorId = Guid.NewGuid().ToString()
                        });
                        context.Drafts.Add(new Draft
                        {
                            Content = "Test draft 2",
                            QuestionId = Guid.NewGuid(),
                            AuthorId = Guid.NewGuid().ToString()
                        });
                        context.SaveChanges();
                    }
                }
            }

            app.UseExceptionHandler(configure =>
            {
                configure.Run(async context =>
                {
                    var exceptionHandlingFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if(exceptionHandlingFeature != null)
                    {
                        string errorMessage = JsonSerializer.Serialize(new
                        {
                            StatusCode = (int)HttpStatusCode.InternalServerError,
                            Message = "Internal Server Error"
                        });
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(errorMessage);
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
