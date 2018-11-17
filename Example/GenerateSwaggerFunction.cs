using System.Net.Http;
using System.Reflection;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.AzureFunctions.Annotations;
using Swashbuckle.AspNetCore.AzureFunctions.Extensions;
using Swashbuckle.AspNetCore.Swagger;

namespace Example {
	public static class GenerateSwaggerFunction {
		[FunctionName("GenerateSwaggerFunction")]
		[SwaggerIgnore]
		public static HttpResponseMessage Run(
			[HttpTrigger(AuthorizationLevel.Function, "get", Route = "swagger/v1")]
			HttpRequestMessage req, ILogger log) {
			var services = new ServiceCollection();

			var functionAssembly = Assembly.GetExecutingAssembly();
			services.AddAzureFunctionsApiProvider(functionAssembly);

			// Add Swagger Configuration
			services.AddSwaggerGen(options => {
				// SwaggerDoc - API
				options.SwaggerDoc("v1", new Info {
					Title = "Example API",
					Version = "v1",
					Description = "Example API",
					Contact = new Contact {
						Name = "Ben Herila",
						Email = "ben@herila.net"
					}
				});

				// Add Enums to Swagger as String
				options.DescribeAllEnumsAsStrings();
				options.EnableAnnotations();
			});

			var serviceProvider = services.BuildServiceProvider(true);
			var content = serviceProvider.GetSwagger("v1");

			return new HttpResponseMessage {
				Content = new StringContent(content)
			};
		}
	}
}
