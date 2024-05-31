using AmzErp.Common.Attribute.Service;
using AmzErp.Common.Mvc;
using AmzErp.Common.Mvc.Filters;
using AmzErp.Common.Mvc.JsonConverter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CommonExtensions
    {
        public static IServiceCollection AddAutoDependencyInjection(this IServiceCollection services)
        {
            var serviceTypes = Assembly.GetCallingAssembly().GetTypes().Where(x => x.GetCustomAttribute<ServiceAttribute>() != null);
            foreach (var type in serviceTypes)
            {
                var serviceAttribute = type.GetCustomAttribute<ServiceAttribute>();
                var serviceType = serviceAttribute.ServiceType ?? type;
                var serviceDescriptor = new ServiceDescriptor(serviceAttribute.ServiceType ?? type, serviceAttribute.ImplementationType ?? type, serviceAttribute.ServiceLifetime);
                services.Add(serviceDescriptor);

            }
            return services;
        }

       

    }
}
