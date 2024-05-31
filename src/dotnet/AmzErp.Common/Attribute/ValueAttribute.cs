using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmzErp.Common.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValueAttribute : AutowiredAttribute
    {
        public string ConfigurationPath { get; set; }
        public ValueAttribute(string configurationPath)
        {
            this.ConfigurationPath = configurationPath;
        }
    }
}
