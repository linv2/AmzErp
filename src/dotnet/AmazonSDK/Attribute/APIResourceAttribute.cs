using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonSDK.Attribute
{
    internal class APIResourceAttribute : System.Attribute
    {
        public string ResourceUri { get; }
        public Method Method { get; }
        public APIResourceAttribute(string resourceUri, Method method)
        {
            this.ResourceUri = resourceUri;
            this.Method = method;
        }
    }
}
