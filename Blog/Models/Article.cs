using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    /// <summary>
    /// 文章
    /// </summary>
    public class Article
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        /// <summary>
        /// 展示的创建时间
        /// </summary>
        public DateTime DisplayCreatedTime { get; set; }

        public DateTime CreatedTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int Enable { get; set; }
    }
}