using AmazonSDK;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonSDK.StaticConfigResourse
{
    public class RegionMarketplaceId
    {
        /// <summary>
        /// /加拿大
        /// </summary>
        public static readonly RegionMarketplaceId CA = new RegionMarketplaceId("加拿大", "A2EUQ1WTGCTBG2", "CA");
        /// <summary>
        /// /美国
        /// </summary>
        public static readonly RegionMarketplaceId US = new RegionMarketplaceId("美国", "ATVPDKIKX0DER", "US");
        /// <summary>
        /// /墨西哥
        /// </summary>
        public static readonly RegionMarketplaceId MX = new RegionMarketplaceId("墨西哥", "A1AM78C64UM0Y8", "MX");
        /// <summary>
        /// /巴西
        /// </summary>
        public static readonly RegionMarketplaceId BR = new RegionMarketplaceId("巴西", "A2Q3Y263D00KWC", "BR");
        /// <summary>
        /// /西班牙
        /// </summary>
        public static readonly RegionMarketplaceId ES = new RegionMarketplaceId("西班牙", "A1RKKUPIHCS9HS", "ES");
        /// <summary>
        /// /英国
        /// </summary>
        public static readonly RegionMarketplaceId GB = new RegionMarketplaceId("英国", "A1F83G8C2ARO7P", "GB");
        /// <summary>
        /// /法国
        /// </summary>
        public static readonly RegionMarketplaceId FR = new RegionMarketplaceId("法国", "A13V1IB3VIYZZH", "FR");
        /// <summary>
        /// /荷兰
        /// </summary>
        public static readonly RegionMarketplaceId NL = new RegionMarketplaceId("荷兰", "A1805IZSGTT6HS", "NL");
        /// <summary>
        /// /德国
        /// </summary>
        public static readonly RegionMarketplaceId DE = new RegionMarketplaceId("德国", "A1PA6795UKMFR9", "DE");
        /// <summary>
        /// /意大利
        /// </summary>
        public static readonly RegionMarketplaceId IT = new RegionMarketplaceId("意大利", "APJ6JRA9NG5V4", "IT");
        /// <summary>
        /// /土耳其
        /// </summary>
        public static readonly RegionMarketplaceId TR = new RegionMarketplaceId("土耳其", "A33AVAJ2PDY3EV", "TR");
        /// <summary>
        /// /阿拉伯联合酋长国
        /// </summary>
        public static readonly RegionMarketplaceId AE = new RegionMarketplaceId("阿拉伯联合酋长国", "A2VIGQ35RCS4UG", "AE");
        /// <summary>
        /// /印度
        /// </summary>
        public static readonly RegionMarketplaceId IN = new RegionMarketplaceId("印度", "A21TJRUUN4KGV", "IN");
        /// <summary>
        /// /新加坡
        /// </summary>
        public static readonly RegionMarketplaceId SG = new RegionMarketplaceId("新加坡", "A19VAU5U5O7RUS", "SG");
        /// <summary>
        /// /澳大利亚
        /// </summary>
        public static readonly RegionMarketplaceId AU = new RegionMarketplaceId("澳大利亚", "A39IBJ37TRP1C6", "AU");
        /// <summary>
        /// /日本
        /// </summary>
        public static readonly RegionMarketplaceId JP = new RegionMarketplaceId("日本", "A1VC38T7YXB528", "JP");



        public RegionMarketplaceId(string countryName, string marketplaceId, string regionCode)
        {
            this.CountryName = countryName;
            this.MarketplaceId = marketplaceId;
            this.RegionCode = regionCode;
        }
        /// <summary>
        /// 国家/地区
        /// </summary>
        public string CountryName { get; set; }
        /// <summary>
        /// marketplaceId
        /// </summary>
        public string MarketplaceId { get; set; }
        /// <summary>
        /// 地区代码
        /// </summary>
        public string RegionCode { get; set; }
    }
}
