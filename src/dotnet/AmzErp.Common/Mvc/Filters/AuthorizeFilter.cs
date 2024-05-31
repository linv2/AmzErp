using AmzErp.Common.Exception;
using AmzErp.DataAccess.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using AmzErp.Common.Attribute;
using AmzErp.DataAccess;

namespace AmzErp.Common.Mvc.Filters
{
    public class AuthorizeFilter : IActionFilter
    {
        public AmzErpDbContext dbContext { get; }
        public AuthorizeFilter(AmzErp.DataAccess.AmzErpDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controllerActionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            if (controllerActionDescriptor.MethodInfo.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                return;
            }
            var userId = context.HttpContext.Session.GetInt32("userId") ?? 0;
            if (userId.Equals(0))
            {
                throw new PermissionException("当前身份未授权");
            }
            var roleAttribute = controllerActionDescriptor.MethodInfo.GetCustomAttributes<RoleAttribute>().FirstOrDefault();
            if (roleAttribute != null && roleAttribute.UserType != null)
            {
                var userTypeValue = context.HttpContext.Session.GetInt32("userType") ?? -1;
                var userType = (UserType)userTypeValue;
                if (!roleAttribute.UserType.Contains(userType))
                {
                    throw new PermissionException("当前身份未授权", 401);
                }
            }
            var userInfo = dbContext.User.FirstOrDefault(x => x.Id.Equals(userId));
            context.HttpContext.Items["user"] = userInfo;

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
