using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonSDK.Models
{
    public class AmazonResponseModel
    {
    }
    public class AmazonResponseModel<TResponse> : AmazonResponseModel
    {
        public TResponse PayLoad { get; set; }
    }
}
