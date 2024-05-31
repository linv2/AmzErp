using System;
using System.Collections.Generic;
using System.Text;

namespace AmzErp.Common.Result
{
    public class ResultData<T> : ResultData
    {
        /// <summary>
        /// <![CDATA[数据]]>
        /// </summary>
        public virtual T Result { get; set; }
    }
}