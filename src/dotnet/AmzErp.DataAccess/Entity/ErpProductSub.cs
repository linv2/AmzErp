using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace AmzErp.DataAccess.Entity
{
    public class ErpProductSub
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ErpProductId { get; set; }
        [ForeignKey("ErpProduct")]
        [JsonIgnore]
        public ErpProduct ErpProduct { get; set; }

        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        [JsonIgnore]
        public User User { get; set; }

        [Display(Name ="属性名"),Required,MaxLength(255)]
        public string AttrName { get; set; }

        [Display(Name = "商品SKU"),MaxLength(255)]
        public string ErpProductSubSKU { get; set; }
    
        [Display(Name ="库存"),Required]
        public int Instock { get; set; }


        [Display(Name ="货币类型"),Required,MaxLength (20)]
        public string Currency { get; set; }

        [Display(Name = "成本价"), Required]
        public decimal CostPrice { get; set; }


        [Display(Name ="销售价"),Required]
        public decimal Price { get; set; }

        [Display(Name = "重量"), Required]
        public int Weight { get; set; }

        [Display(Name ="图片"),MaxLength(2000)]
        public string Images { get; set; }
    }
}
