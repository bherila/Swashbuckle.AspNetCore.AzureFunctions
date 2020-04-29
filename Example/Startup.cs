using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.AzureFunctions.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;
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
                //options.EnableAnnotations();
            });

            builder.Services.AddTransient<Microsoft.AspNetCore.Hosting.IHostingEnvironment, HostingEnvironment>();

            //ServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
            //var provider3 = serviceProvider.GetService<IConfigureOptions<SwaggerGeneratorOptions>>();
            //var provider = serviceProvider.GetService<Swashbuckle.AspNetCore.Swagger.ISwaggerProvider>();
            //var provider2 = serviceProvider.GetService<SwaggerGenerator>();

            //services.AddSwaggerGenNewtonsoftSupport();
        }
    }

    public class HostingEnvironment : Microsoft.AspNetCore.Hosting.IHostingEnvironment
    {
        public string EnvironmentName { get; set; }

        public string WebRoot { get; set; }
        public string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string WebRootPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IFileProvider WebRootFileProvider { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ContentRootPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IFileProvider ContentRootFileProvider { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
