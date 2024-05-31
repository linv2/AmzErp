using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmzErp.Common.Attribute
{
    public class JNActionAttribute : HttpMethodAttribute
    {
        public JNActionAttribute() : base(new string[] { "GET", "POST" }, "/html/[controller]/[action].html")
        {

        }
    }
}
