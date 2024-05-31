using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace AmazonSDK.Models.Request.Feeds
{
    /// <summary>
    /// https://images-na.ssl-images-amazon.com/images/G/01/rainier/help/xsd/release_4_1/Product.xsd
    /// </summary>
    public class CreateProductFeedRequest
    {
        /// <summary>
        /// SKU
        /// </summary>
        [MinLength(1), MaxLength(40)]
        public string SKU { get; set; }

        /// <summary>
        /// EAN/UPC/ISBN/ASIN
        /// </summary>
        public StandardProduct StandardProductID { get; set; }

        /// <summary>
        /// 亚马逊GTIN免税原因
        /// </summary>
        public string GtinExemptionReason { get; set; }

        /// <summary>
        /// 产品税号，xsd上要求必须填，网上说没有可以不填
        /// </summary>
        public string ProductTaxCode { get; set; }
        /// <summary>
        /// 产品发布时间
        /// </summary>
        public DateTime? LaunchDate { get; set; }

        /// <summary>
        /// 停产日期
        /// </summary>
        public DateTime? DiscontinueDate { get; set; }

        /// <summary>
        /// 预售日期
        /// </summary>
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// 外链商品Url
        /// </summary>
        public string ExternalProductUrl { get; set; }


        /// <summary>
        /// 新旧程度
        /// </summary>
        public ConditionInfo Condition { get; set; }

        /// <summary>
        /// 返利
        /// </summary>
        public RebateType Rebate { get; set; }

        /// <summary>
        /// <para>一项商品含的包裹数量</para>
        /// <para>使用此字段可指明您要出售的商品中包含的单位数量，以便每个单位都打包以供单独销售。</para>
        /// <para> Use this field to indicate the number of units included in the item you are offering for sale, such that each unit is packaged for individual sale. </para>
        /// </summary>
        public int ItemPackageQuantity { get; set; } = 1;
        /// <summary>
        /// <para>使用此字段来指示您提供的待售商品中包含的离散商品的数量，这样每件商品都不会单独销售。</para>
        /// <para>例如，如果您销售一箱 10 包袜子，并且每包包含 3 双袜子，则该箱的 ItemPackageQuantity = 10 和 NumberOfItems = 30。</para>
        /// <para>Use this field to indicate the number of discrete items included in the item you are offering for sale, such that each item is not packaged for individual sale. </para>
        /// <para>For example, if you are selling a case of 10 packages of socks, and each package contains 3 pairs of socks, the case would have ItemPackageQuantity = 10 and NumberOfItems = 30.</para>
        /// </summary>
        public int NumberOfItems { get; set; } = 1;

        //液体尺寸用不到，先不写，
        //public string LiquidVolume { get; set; }

        /// <summary>
        /// 描述数据
        /// </summary>
        public DescriptionData DescriptionData { get; set; }

        /// <summary>
        /// 产品数据
        /// </summary>
        public ProductData ProductData { get; set; }
    }
    public class StandardProduct
    {
        /// <summary>
        ///EAN/UPC/ISBN/ASIN
        /// </summary>
        public string Type { get; set; }
        public string Value { get; set; }
    }
    /// <summary>
    /// 新旧程度
    /// </summary>
    public class ConditionInfo
    {
        /// <summary>
        /// restriction
        /// </summary>
        public string ConditionType { get; set; }
        [MaxLength(2000)]
        public string ConditionValue { get; set; }
    }
    public class RebateType
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime RebateStartDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime RebateEndDate { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string RebateMessage { get; set; }
        /// <summary>
        /// 名字
        /// </summary>
        public string RebateName { get; set; }
    }

    public class DescriptionData
    {
        [Required]
        public string Title { get; set; }
        [XmlElement(IsNullable = true)]
        public string Brand { get; set; }
        [Required]
        public string Designer { get; set; }
        [XmlElement(IsNullable = true)]
        [MaxLength(2000)]
        public string Description { get; set; }
    }

    public class ProductData { }
}
