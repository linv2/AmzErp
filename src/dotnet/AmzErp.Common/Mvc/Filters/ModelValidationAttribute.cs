using AmzErp.Common.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;

namespace AmzErp.Common.Mvc.Filters
{
    public class ModelValidationAttribute : System.Attribute, IActionFilter
    {
        private readonly StringEqualityComparer _stringEqualityComparer = new StringEqualityComparer();
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!(context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor))
            {
                return;
            }

            var modelState = context.ModelState;
            if (!modelState.IsValid)
            {
                foreach (var parameter in controllerActionDescriptor.MethodInfo.GetParameters())
                {
                    var bindAttribute = parameter.GetCustomAttribute<BindAttribute>();
                    if (bindAttribute == null || bindAttribute.Include == null || bindAttribute.Include.Length == 0)
                    {
                        var message = modelState.FirstOrDefault().Value.Errors.FirstOrDefault()?.ErrorMessage;
                        context.Result = new OkObjectResult(ResultData.Fail(message));
                        return;
                    }
                    else
                    {
                        foreach (var modelStateKey in modelState.Keys)
                        {
                            if (bindAttribute.Include?.Contains(modelStateKey, _stringEqualityComparer) ?? false)
                            {
                                var entity = modelState[modelStateKey];
                                var message = entity.Errors.FirstOrDefault()?.ErrorMessage;
                                context.Result = new OkObjectResult(ResultData.Fail(message));
                                return;
                            }
                        }
                    }
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }

    class StringEqualityComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            return x.Equals(y, StringComparison.CurrentCultureIgnoreCase);
        }

        public int GetHashCode(string obj)
        {
            return obj.GetHashCode();
        }
    }
}
