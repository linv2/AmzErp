using AmazonSDK.StaticConfigResourse;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonSDK
{
    public class AWSAuthenticationCredentials
    {

        /// <summary>
        /// AWS IAM User Access Key Id
        /// </summary>
        public string AccessKeyId { get; set; }

        /// <summary>
        /// AWS IAM User Secret Key
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// AWS Region
        /// </summary>
        public SellerRegion SellerRegion { get; set; }
    }
}
