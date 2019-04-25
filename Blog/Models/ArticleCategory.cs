using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    /// <summary>
    /// 文章&分类关系表
    /// </summary>
    public class ArticleCategory
    {
        public int CategoryId { get; set; }
        public int ArticleId { get; set; }
        public int Sort { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int Enable { get; set; }
    }
}