using AmazonSDK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmazonSDK.SDKException
{
    public class ClientException : ApplicationException
    {
        public List<AmazonResponseErrorItem> Errors { get; private set; }
        internal ClientException(AmazonResponseError amazonResponseError) : base(string.Join(System.Environment.NewLine, amazonResponseError.Errors.Select(x => x.Message)))
        {
            this.Errors = amazonResponseError.Errors;
        }
    }
}
