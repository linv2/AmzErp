using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonSDK.StaticConfigResourse
{
    public class SellerRegion
    {
        /// <summary>
        /// 北美（加拿大、美国、墨西哥和巴西商城）
        /// </summary>
        public static readonly SellerRegion NORTH_AMERICA = new SellerRegion("https://sellingpartnerapi-na.amazon.com", "us-east-1");
        /// <summary>
        /// 欧洲（西班牙、英国、法国、荷兰、德国、意大利、土耳其、阿联酋和印度商城）
        /// </summary>
        public static readonly SellerRegion EUROPE = new SellerRegion("https://sellingpartnerapi-eu.amazon.com", "eu-west-1");
        /// <summary>
        /// 远东（新加坡、澳大利亚和日本商城）
        /// </summary>
        public static readonly SellerRegion FAR_EAST = new SellerRegion("https://sellingpartnerapi-fe.amazon.com", "us-west-2");


        /// <summary>
        /// [沙盒]北美（加拿大、美国、墨西哥和巴西商城）
        /// </summary>
        public static readonly SellerRegion SANDBOX_NORTH_AMERICA = new SellerRegion("https://sandbox.sellingpartnerapi-na.amazon.com", "us-east-1");
        /// <summary>
        /// [沙盒]欧洲（西班牙、英国、法国、荷兰、德国、意大利、土耳其、阿联酋和印度商城）
        /// </summary>
        public static readonly SellerRegion SANDBOX_EUROPE = new SellerRegion("https://sandbox.sellingpartnerapi-eu.amazon.com", "eu-west-1");
        /// <summary>
        /// [沙盒]远东（新加坡、澳大利亚和日本商城）
        /// </summary>
        public static readonly SellerRegion SANDBOX_FAR_EAST = new SellerRegion("https://sandbox.sellingpartnerapi-fe.amazon.com", "us-west-2");

        public static IList<SellerRegion> SellerRegionList { get; set; }

        public static IList<SellerRegion> SandBoxSellerRegionList { get; set; }

        static SellerRegion()
        {
            SellerRegionList.Add(NORTH_AMERICA);
            SellerRegionList.Add(EUROPE);
            SellerRegionList.Add(FAR_EAST);

            SandBoxSellerRegionList.Add(SANDBOX_NORTH_AMERICA);
            SandBoxSellerRegionList.Add(SANDBOX_EUROPE);
            SandBoxSellerRegionList.Add(SANDBOX_FAR_EAST);
        }
        internal SellerRegion(string endPoint, string region)
        {
            this.EndPoint = new Uri(endPoint);
            this.Region = region;
        }
        /// <summary>
        /// 端点
        /// </summary>
        public Uri EndPoint { get; set; }
        /// <summary>
        /// AWS 区域
        /// </summary>

        public string Region { get; set; }
    }
}
