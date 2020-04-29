using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.AzureFunctions.Extensions;
using System;
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

            builder.Services.AddSingleton<Microsoft.AspNetCore.Hosting.IHostingEnvironment>(s =>
            {
                var env = s.GetService<Microsoft.Extensions.Hosting.IHostingEnvironment>();
                return new HostingEnvironment
                {
                    ApplicationName = env.ApplicationName,
                    EnvironmentName = env.EnvironmentName,
                    ContentRootPath = env.ContentRootPath,
                    ContentRootFileProvider = env.ContentRootFileProvider,
                };
            });

            services.AddSwaggerGenNewtonsoftSupport();
        }
    }

    public class HostingEnvironment : Microsoft.AspNetCore.Hosting.IHostingEnvironment
    {
        public string EnvironmentName { get; set; }

        public string WebRoot { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ApplicationName { get; set; }
        public string WebRootPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IFileProvider WebRootFileProvider { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ContentRootPath { get; set; }
        public IFileProvider ContentRootFileProvider { get; set; }
    }
}
