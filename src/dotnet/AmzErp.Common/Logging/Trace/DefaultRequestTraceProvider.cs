using System;
using System.Collections.Generic;
using System.Text;

namespace AmzErp.Common.Logging.Trace
{
    public class DefaultRequestTraceProvider : IRequestTrace
    {
        public void Clear(string propertyName = "reqId")
        {
        }

        public string GetTraceId(string propertyName = "reqId")
        {
            return null;
        }

        public void Trace(string propertyName = "reqId")
        {
        }
    }
}
