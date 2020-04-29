//using System;
//using System.Linq;
//using Microsoft.AspNetCore.Http;
//using Microsoft.OpenApi.Models;
//using Swashbuckle.AspNetCore.Swagger;
//using Swashbuckle.AspNetCore.SwaggerGen;

//namespace Swashbuckle.AspNetCore.AzureFunctions.Filters
//{
//    public class RemoveBodyParameterFromGetFilter : IOperationFilter
//    {
//        public void Apply(OpenApiOperation operation, OperationFilterContext context)
//        {
//            if (string.Equals(context.ApiDescription.HttpMethod, HttpMethods.Get, StringComparison.OrdinalIgnoreCase))
//            {
//                var bodyParameters = operation.Parameters.Where(x => x.In == ParameterLocation. "body").ToList();
//                foreach (var bodyParameter in bodyParameters)
//                {
//                    operation.Parameters.Remove(bodyParameter);
//                }
//            }
//        }
//    }
//}