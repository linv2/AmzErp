using System;
using System.Collections.Generic;
using System.Text;

namespace AmzErp.Common.Exception
{
    public class PermissionException : ApiException
    {
        public PermissionException(string message, int code = 403) : base(message, code)
        {
        }
    }
}
