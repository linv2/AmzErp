using AmzErp.Common.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmzErp.Common.Exception
{
    public class ApiException : ApplicationException
    {
        public Result.ResultData Result { get; private set; }

        public ApiException(string message, int code = 400) : base(message)
        {
            Result = new Result.ResultData()
            {
                Code = code,
                Message = message
            };
        }
    }
}