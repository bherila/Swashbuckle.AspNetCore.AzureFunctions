using System.Reflection;

namespace Swashbuckle.AspNetCore.AzureFunctions.Application
{
    public class AzureFunctionsOptions
    {
        public Assembly Assembly { get; set; }

        /// <summary>
        /// The route prefix that applies to all HTTP Trigger routes.
        /// Default: "api"
        /// </summary>
        /// <remarks>
        /// If you have modified the route prefix in your function's host.json 
        /// file, it should be changed here to match. Use an empty string to
        /// remove the default prefix.
        /// See also: https://docs.microsoft.com/en-us/azure/azure-functions/functions-host-json#http
        /// </remarks>
        public string RoutePrefix { get; set; } = "api";
    }
}