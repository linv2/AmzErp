using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmzErp.Common.Attribute.Service
{
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class ServiceAttribute : System.Attribute
    {
        public ServiceLifetime ServiceLifetime { get; }
       public Type ServiceType { get; }

        public Type ImplementationType { get;  }
        public ServiceAttribute(ServiceLifetime serviceLifetime, Type serviceType=null, Type implementationType = null)
        {
            ServiceLifetime = serviceLifetime;
            ServiceType = serviceType;
            ImplementationType = implementationType;
        }
    }
}
