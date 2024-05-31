using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonSDK.Models.Request.Feeds
{
    public class CreateFeedSpecificationRequest
    {
        /// <summary>
        /// The feed type.
        /// </summary>
        [JsonProperty("feed_type")]
        public string FeedType { get; set; }
        /// <summary>
        /// A list of identifiers for marketplaces that you want the feed to be applied to.
        /// </summary>
        [JsonProperty("marketplace_ids")]
        public List<string> MarketplaceIds { get; set; }
        /// <summary>
        /// 	The document identifier returned by the createFeedDocument operation. Encrypt and upload the feed document contents before calling the createFeed operation.
        /// </summary>
        [JsonProperty("input_feed_document_id")]
        public string InputFeedDocumentId { get; set; }
        /// <summary>
        /// Additional options to control the feed. These vary by feed type.
        /// </summary>
        [JsonProperty("feed_options")]
        public IDictionary<string, string> FeedOptions { get; set; }
    }
}
