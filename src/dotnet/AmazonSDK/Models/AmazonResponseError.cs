using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonSDK.Models
{
    public class AmazonResponseError
    {
        public List<AmazonResponseErrorItem> Errors { get; set; }
    }

    public class AmazonResponseErrorItem
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}
