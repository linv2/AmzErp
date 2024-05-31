using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace AmzErp.DataAccess.Entity
{
    public class ErpProductInfo
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

        [Display(Name ="标题"),Required,MaxLength(500)]
        public string Title { get; set; }

        [Display(Name = "短标题"), MaxLength(255)]
        public string ShortTitle { get; set; }

        [Display(Name = "关键字"), Required, MaxLength(1000)]
        public string KeyWords { get; set; }

        [Display(Name ="要点说明"),MaxLength(2000)]
        public string Shine { get; set; }
        [Display(Name ="描述"),DataType(DataType.Text)]
        public string Description { get; set; }
        [Display(Name ="语言"),Required,MaxLength(10)]
        public string Language { get; set; }

    }
}
