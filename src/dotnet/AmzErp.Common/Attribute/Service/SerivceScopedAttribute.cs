using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmzErp.Common.Attribute.Service
{
    public class SerivceScopedAttribute : ServiceAttribute
    {
        public SerivceScopedAttribute(Type serviceType = null, Type implementationType = null) : base(ServiceLifetime.Scoped,serviceType,implementationType)
        {
        }
    }
}
