using System;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.AzureFunctions.Annotations;
using Swashbuckle.AspNetCore.AzureFunctions.Extensions;

namespace Example
{
	public class GenerateSwaggerFunction
	{
		private readonly IServiceProvider _serviceProvider;

		public GenerateSwaggerFunction(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		[FunctionName("GenerateSwaggerFunction")]
		[SwaggerIgnore]
		public HttpResponseMessage Run(
			[HttpTrigger(AuthorizationLevel.Function, "get", Route = "swagger/v1")]
			HttpRequestMessage req, ILogger log)
		{
			var content = _serviceProvider.GetSwagger("v1");

			return new HttpResponseMessage
			{
				Content = new StringContent(content)
			};
		}
	}
}
