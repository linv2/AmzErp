using System;
using System.Collections.Generic;
using System.Text;

namespace AmzErp.Common.Result
{

    public class ResultArray<TData> : ResultData<IEnumerable<TData>>
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }
    }
}