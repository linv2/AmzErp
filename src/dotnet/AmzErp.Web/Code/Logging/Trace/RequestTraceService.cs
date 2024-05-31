using AmzErp.Common.Logging.Trace;
using AmzErp.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmzErp.Web.Code.Logging.Trace
{
    public class RequestTraceService : IRequestTrace
    {
        public void Clear(string propertyName = "reqId")
        {
            log4net.ThreadContext.Properties.Remove(propertyName);
        }

        public string GetTraceId(string propertyName = "reqId")
        {
            return Convert.ToString(log4net.ThreadContext.Properties[propertyName]);
        }

        public void Trace(string propertyName = "reqId")
        {
            log4net.ThreadContext.Properties[propertyName] = Snowflake.Instance.nextId();
        }
    }
}
