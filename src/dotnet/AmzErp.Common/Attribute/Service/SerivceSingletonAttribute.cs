using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmzErp.Common.Attribute.Service
{
    public class SerivceSingleton : ServiceAttribute
    {
        public SerivceSingleton(Type serviceType = null, Type implementationType = null) : base(ServiceLifetime.Singleton, serviceType,implementationType)
        {
        }
    }
}
