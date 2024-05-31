using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AmzErp.Common.Attribute;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using Newtonsoft.Json;

namespace AmzErp.Common.Mvc
{

    public class ControllerActivator : IControllerActivator
    {
        public object Create(ControllerContext actionContext)
        {
            if (actionContext == null)
                throw new ArgumentNullException(nameof(actionContext));

            Type serviceType = actionContext.ActionDescriptor.ControllerTypeInfo.AsType();
            var target = actionContext.HttpContext.RequestServices.GetRequiredService(serviceType);
            var properties = serviceType.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(x => x.GetCustomAttribute<AutowiredAttribute>() != null);
            foreach (PropertyInfo info in properties)
            { //配置值注入
                if (info.GetCustomAttribute<ValueAttribute>() is ValueAttribute valueAttribute)
                {
                    var configurationPath = valueAttribute.ConfigurationPath;
                    if (actionContext.HttpContext.RequestServices.GetService(typeof(IConfiguration)) is IConfiguration configService)
                    {

                        var pathValue = configService.GetSection(configurationPath).Value;
                        if (pathValue != null)
                        {
                            var pathV = Convert.ChangeType(pathValue, info.PropertyType);
                            info.SetValue(target, pathV);
                        }


                    }

                }
                else if (info.GetCustomAttribute<AutowiredAttribute>() != null)
                {
                    var propertyType = info.PropertyType;
                    var impl = actionContext.HttpContext.RequestServices.GetService(propertyType);
                    if (impl != null)
                    {
                        info.SetValue(target, impl);
                    }
                }


            }
            return target;
        }

        public void Release(ControllerContext context, object controller)
        {
        }
    }
}
