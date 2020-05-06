using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Writers;
using Swashbuckle.AspNetCore.AzureFunctions.Application;
using Swashbuckle.AspNetCore.AzureFunctions.Providers;
using Swashbuckle.AspNetCore.Swagger;

namespace Swashbuckle.AspNetCore.AzureFunctions.Extensions
{
    /// <summary>
    /// Azure functions swagger extensions for <see cref="IServiceCollection"/> and <see cref="IServiceProvider"/>
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Initialize <see cref="IApiDescriptionGroupCollectionProvider"/> for Azure Functions
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="functionAssembly">Functions assembly</param>
        /// <param name="routePrefix">HTTP functions route prefix</param>
        public static void AddAzureFunctionsApiProvider(this IServiceCollection services, Assembly functionAssembly, string routePrefix = "api")
        {
            services.AddOptions();
            services.Configure<AzureFunctionsOptions>(o =>
            {
                o.Assembly = functionAssembly;
                o.RoutePrefix = routePrefix;
            });
            services.AddSingleton<IApiDescriptionGroupCollectionProvider, FunctionApiDescriptionProvider>();
        }

        /// <summary>
        /// Gets swagger json
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="documentName"></param>
        /// <param name="serializeAsV2"></param>
        /// <returns></returns>
        public static string GetSwagger(this IServiceProvider serviceProvider, string documentName, bool serializeAsV2 = false)
        {
            var requiredService = serviceProvider.GetRequiredService<ISwaggerProvider>();
            var swagger = requiredService.GetSwagger(documentName);

            using (var textWriter = new StringWriter(CultureInfo.InvariantCulture))
            {
                var jsonWriter = new OpenApiJsonWriter(textWriter);
                if (serializeAsV2) swagger.SerializeAsV2(jsonWriter); else swagger.SerializeAsV3(jsonWriter);

                return textWriter.ToString();
            }
        }
    }
}