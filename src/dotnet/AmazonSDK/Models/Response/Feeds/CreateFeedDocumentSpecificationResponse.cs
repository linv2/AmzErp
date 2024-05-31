using AmazonSDK.Attribute;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonSDK.Models.Request.Feeds
{
    public class CreateFeedDocumentSpecificationResponse
    {
        public string FeedDocumentId { get; set; }
        public string Url { get; set; }
        public FeedDocumentEncryptionDetails EncryptionDetails { get; set; }
    }

    public class FeedDocumentEncryptionDetails
    {
        public Standard Standard { get; set; }
        public string Key { get; set; }
        public string InitializationVector { get; set; }
    }
    public enum Standard
    {
        AES
    }

}


