using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmzErp.Common.Result
{
    public class ResultData
    {
        public static ResultData Ok => Success();
        /// <summary>
        /// <![CDATA[错误码]]>
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        ///<![CDATA[消息]]>
        /// </summary>
        public string Message { get; set; }

        public static ResultData Success(string message = "操作成功", int code = 200)
        {
            return new ResultData()
            {
                Code = code,
                Message = message
            };
        }
        public static ResultData Fail(string message = "操作失败", int code = 400)
        {
            return new ResultData()
            {
                Code = code,
                Message = message
            };
        }
        public static ResultData<T> Data<T>(T data, string message = "操作成功", int code = 200)
        {
            return new ResultData<T>()
            {
                Result = data,
                Code = code,
                Message = message
            };
        }
        public static ResultArray<T> Success<T>(IEnumerable<T> enumerable, int total)
        {
            return new ResultArray<T>()
            {
                Result = enumerable,
                Total = total,
                Code = 200,
            };
        }
    }
}
