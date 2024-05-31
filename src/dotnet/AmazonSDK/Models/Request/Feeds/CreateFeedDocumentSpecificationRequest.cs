using AmazonSDK.Attribute;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonSDK.Models.Request.Feeds
{
    [APIResource("/feeds/2020-09-04/documents", RestSharp.Method.POST)]
    public class CreateFeedDocumentSpecificationRequest : AmazonRequestModel<CreateFeedDocumentSpecificationResponse>
    {
        /// <summary>
        /// The content type of the feed
        /// </summary>
        [JsonProperty("contentType")]
        public string ContentType
        {
            get; set;
        } = "application/json";
    }

}


