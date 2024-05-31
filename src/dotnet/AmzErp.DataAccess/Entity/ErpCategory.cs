using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmzErp.DataAccess.Entity
{
    public class ErpCategory
    {
        [Key] public int Id { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        ///     父Id
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        ///     子节点数量
        /// </summary>
        [Required]
        public int ChildCount { get; set; }

        /// <summary>
        ///     深度
        /// </summary>
        [Required]
        public int Depth { get; set; }

        /// <summary>
        ///     关系树
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Tree { get; set; }

        /// <summary>
        ///     是否启用
        /// </summary>
        [Required]
        public bool IsEndable { get; set; }

        /// <summary>
        ///     删除状态
        /// </summary>
        [Required]
        public bool Delete { get; set; }

        [Required]
        public DateTime CreatedTime { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")] public User User { get; set; }
    }
}