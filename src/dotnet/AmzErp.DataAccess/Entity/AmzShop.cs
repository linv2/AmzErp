using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AmzErp.DataAccess.Entity
{
    /// <summary>
    /// 店铺信息
    /// </summary>
    public class AmzShop
    {
        [Key]
        public int Id { get; set; }
    
        /// <summary>
        /// 名字
        /// </summary>
        [Required,MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string RefreshToken { get; set; }
    
        /// <summary>
        /// 亚马逊卖家ID
        /// </summary>
       [MaxLength(50)]
        public string Selling_Partner_Id { get; set; }
    
        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 认证时间
        /// </summary>
        public DateTime? AuthorizationTime { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        public bool Endable { get; set; }



        /// <summary>
        /// 区域总数量
        /// </summary>
        [Required]
        public int RegionCount { get; set; }
        /// <summary>
        /// 用户授权回调
        /// </summary>
        public string HashKey { get; set; }


        public int UserId { get; set; }
        [ForeignKey("UserId")]
        [JsonIgnore]
        public User User { get; set; }
    }
}
