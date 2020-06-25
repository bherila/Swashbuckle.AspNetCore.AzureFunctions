﻿using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.AzureFunctions.Extensions;
using System.Reflection;

[assembly: FunctionsStartup(typeof(Example.Startup))]
namespace Example
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var services = builder.Services;

            var functionAssembly = Assembly.GetExecutingAssembly();
            services.AddAzureFunctionsApiProvider(functionAssembly);

            // Add Swagger Configuration
            services.AddSwaggerGen(options =>
            {
                // SwaggerDoc - API
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Example API",
                    Version = "v1",
                    Description = "Example API",
                    Contact = new OpenApiContact
                    {
                        Name = "Ben Herila",
                        Email = "ben@herila.net"
                    }
                });

                // Add Enums to Swagger as String
                options.DescribeAllEnumsAsStrings();
                options.EnableAnnotations();
            });

            services.AddSwaggerGenNewtonsoftSupport();
        }
    }
}
