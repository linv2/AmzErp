using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonSDK.Models
{
    /// <summary>
    /// Detailed information that is specific to a seller in a Marketplace.
    /// </summary>
    public class Participation
    {
        /// <summary>
        /// 
        /// </summary>
        public string isParticipating { get; set; }
        /// <summary>
        /// Specifies if the seller has suspended listings. True if the seller Listing Status is set to Inactive, otherwise False.
        /// </summary>
        public string hasSuspendedListings { get; set; }
    }
}
