using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Swashbuckle.AspNetCore.AzureFunctions.Filters
{
    /// <summary>
    /// Operation filter, that adds http method to OperationId. One azure function can handle multiple http methods
    /// </summary>
    public class AppendHttpMethodToOperationIdFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.OperationId += $"_{context.ApiDescription.HttpMethod}";
        }
    }
}