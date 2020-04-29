using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Example
{
    public static class Extensions
    {
        public static T GetService<T>(this IServiceCollection services) where T : class
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var service = services.FirstOrDefault(s => s.ServiceType == typeof(T));
            if (service == null) return default;

            return service.ImplementationInstance as T;
        }
    }
}
