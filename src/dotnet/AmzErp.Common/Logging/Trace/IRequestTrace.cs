using System;
using System.Collections.Generic;
using System.Text;

namespace AmzErp.Common.Logging.Trace
{
    public interface IRequestTrace
    {
        string GetTraceId(string propertyName = "reqId");
        void Trace(string propertyName = "reqId");

        void Clear(string propertyName = "reqId");
    }
}
