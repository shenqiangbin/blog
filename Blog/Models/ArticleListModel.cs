using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class ArticleListModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Order { get; set; }

        public PublishStatus PublishStatus { get; set; }

        public ArticleListModel()
        {
            this.PageSize = 10;
            this.PublishStatus = PublishStatus.Published;
        }
    }

    public class ArticleListModelResult
    {
        public IList<Article> List { get; set; }
        public int TotalCount { get; set; }
    }
}