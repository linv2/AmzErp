using AmazonSDK.Attribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonSDK.Models.Response.Sellers
{
    public class GetMarketplaceParticipationsResponse
    {
        /// <summary>
        /// Detailed information about an Amazon market where a seller can list items for sale and customers can view and purchase items.
        /// </summary>
        public Marketplace marketplace { get; set; }
        /// <summary>
        /// Detailed information that is specific to a seller in a Marketplace.
        /// </summary>
        public Participation participation { get; set; }
    }


}
