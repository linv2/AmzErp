using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AmzErp.DataAccess.Entity
{
    public class AmzMarketplace
    {
        [Key]
        public int Id { get; set; }
        
        /// <summary>
        /// 市场Id
        /// </summary>
        [Required,MaxLength(50)]
        public string MarketplaceId { get; set; }

        /// <summary>
        /// 国家代码
        /// </summary>
        [Required,MaxLength(50)]
        public string CountryCode { get; set; }
        /// <summary>
        /// Marketplace name
        /// </summary>
        [Required, MaxLength(50)]
        public string Name { get; set; }
        /// <summary>
        /// The ISO 4217 format currency code of the marketplace
        /// </summary>

        [Required, MaxLength(50)]
        public string DefaultCurrencyCode { get; set; }
        /// <summary>
        /// The ISO 639-1 format language code of the marketplace.
        /// </summary

        [Required, MaxLength(50)]
        public string DefaultLanguageCode { get; set; }
        /// <summary>
        /// The domain name of the marketplace
        /// </summary>
        [Required, MaxLength(50)]
        public string DomainName { get; set; }


        #region 外键

        public int AmzShopId { get; set; }
        [ForeignKey("AmzShopId"), JsonIgnore]
        public AmzShop AmzShop { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId"), JsonIgnore]
        public User User { get; set; }

        #endregion 外键
    }
}
