﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Roommates.API.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Meet Your Roommate API",
                    Version = "v1",
                    Description = "Meet Your Roommate RESTful API",
                    Contact = new OpenApiContact { Name = "CodeMaster Team" }
                });
                
                c.EnableAnnotations();
            });
            return services;
        }
        

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "api-docs/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/api-docs/v1/swagger.json", "Meet Your Roommate API V1");
                c.RoutePrefix = "api-docs/v1";
            });
            return app;
        }

        public static IServiceCollection ActiveCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                                  builder =>
                                  {
                                      builder.WithOrigins("https://meetyourroommateapi.azurewebsites.net",
                                          "http://localhost:8080")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                                  });
            });

            return services;
        }
    }
}
