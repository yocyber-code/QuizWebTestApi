using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Quiz.Contracts.DTOs;

namespace Quiz.Core
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            return services
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        }

        public static IServiceCollection AddJwtTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer("Bearer",
              options =>
              {
                  options.Authority = configuration["Authentication:Authority"];
                  options.Audience = configuration["Authentication:Audience"];
                  options.RequireHttpsMetadata = false;
                  options.TokenValidationParameters = new TokenValidationParameters()
                  {
                      NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"//To set Identity.Name 
                  };
                  options.Events = new JwtBearerEvents()
                  {
                      OnChallenge = context =>
                      {
                          if (!context.Response.HasStarted)
                          {
                              context.Response.StatusCode = 403;
                              context.Response.ContentType = "application/json";
                              context.HandleResponse();
                              var result = JsonConvert.SerializeObject(new ApiGenericResult<string>() { Code = 403, Count = 1, Message = "Authorization has been denied for this request." });
                              return context.Response.WriteAsync(result);
                          }
                          else
                          {
                              var result = JsonConvert.SerializeObject(new ApiGenericResult<string>() { Code = 403, Count = 1, Message = "Token Expired" });
                              return context.Response.WriteAsync(result);
                          }
                      },
                      OnForbidden = context =>
                      {
                          context.Response.StatusCode = 403;
                          context.Response.ContentType = "application/json";
                          var result = JsonConvert.SerializeObject(new ApiGenericResult<string>() { Code = 403, Count = 1, Message = "You are not authorized to access this resource" });
                          return context.Response.WriteAsync(result);
                      }
                  };
              });

            return services;

        }
    }
}
