using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace AmzErp.DataAccess.Entity
{
    public class ErpProduct
    {

        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public int Id { get; set; }


        /// <summary>
        /// 分类
        /// </summary>
        [Required]
        public int ErpCategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [JsonIgnore]
        public ErpCategory ErpCategory { get; set; }


        public int UserId { get; set; }
        [ForeignKey("UserId")]
        [JsonIgnore]
        public User User { get; set; }


        /// <summary>
        /// SKU
        /// </summary>
        [Required,MaxLength(255),Display(Name  ="产品SKU")]
        public string ProdSKU { get; set; }


        [MaxLength(500),Display(Name = "中文名称")]
        public string ChinaName { get; set; }
        [MaxLength(500), Display(Name = "英文名称")]
        public string EnglishName { get; set; }

        /// <summary>
        /// SourceUrl
        /// </summary>
        [Display(Name="来源URL"),MaxLength(500)]
        public string SourceUrl { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>

        [Display(Name ="品牌"),MaxLength(255)]
        public string ProdBrand { get; set; }

        /// <summary>
        /// 原产地
        /// </summary>
        [Display(Name = "原产地"),MaxLength(255)]
        public string SourceArea { get; set; }

        /// <summary>
        /// 成人用品
        /// </summary>
        [Display(Name ="成人用品"),Required]
        public bool Aldult { get; set; }







    }
}
