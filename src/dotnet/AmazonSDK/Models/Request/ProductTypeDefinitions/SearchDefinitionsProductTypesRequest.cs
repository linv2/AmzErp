using AmazonSDK.Attribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonSDK.Models.Request.ProductTypeDefinitions
{

    [APIResource("/definitions/2020-09-01/productTypes", RestSharp.Method.GET)]
    public class SearchDefinitionsProductTypesRequest : AmazonRequestModel
    {
        public string keywords { get; set; }
        public string marketplaceIds { get; set; }
    }
}
