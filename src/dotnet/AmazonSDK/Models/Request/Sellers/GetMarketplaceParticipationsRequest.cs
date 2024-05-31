using AmazonSDK.Attribute;
using AmazonSDK.Models.Response.Sellers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonSDK.Models.Request.Sellers
{
    [APIResource("/sellers/v1/marketplaceParticipations", RestSharp.Method.GET)]
    public class GetMarketplaceParticipationsRequest : AmazonRequestModel<List<GetMarketplaceParticipationsResponse>>
    {
        public GetMarketplaceParticipationsRequest()
        {
            
        }
    }
}
