using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonSDK.Models
{
    /// <summary>
    /// Detailed information about an Amazon market where a seller can list items for sale and customers can view and purchase items.
    /// </summary>
    public class Marketplace
    {
        /// <summary>
        /// The encrypted marketplace value.
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// The ISO 3166-1 alpha-2 format country code of the marketplace.
        /// </summary>
        public string countryCode { get; set; }
        /// <summary>
        /// Marketplace name
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// The ISO 4217 format currency code of the marketplace
        /// </summary>
        public string defaultCurrencyCode { get; set; }
        /// <summary>
        /// The ISO 639-1 format language code of the marketplace.
        /// </summary>
        public string defaultLanguageCode { get; set; }
        /// <summary>
        /// The domain name of the marketplace
        /// </summary>
        public string domainName { get; set; }
    }
}
