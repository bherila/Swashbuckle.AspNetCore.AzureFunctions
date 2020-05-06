using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.AzureFunctions.Annotations;
using Swashbuckle.AspNetCore.AzureFunctions.Filters;

namespace Example
{
    public static class Function1
    {
        [FunctionName(nameof(Run))]
        [SwaggerOperation(nameof(Run), Tags = new[] { "Sample" })]
        [SwaggerResponse(type: typeof(string), statusCode: 200)]
        [SupportedMediaType("application/json")]
        [OptionalQueryParameter("name", typeof(string))]
        [SwaggerOperationFilter(typeof(AppendHttpMethodToOperationIdFilter))]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            return req.CreateResponse(HttpStatusCode.OK, "Hello");
        }
    }
}
