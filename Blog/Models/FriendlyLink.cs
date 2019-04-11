using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    /// <summary>
    /// 友情链接
    /// </summary>
    public class FriendlyLink
    {
        public int FriendlyLinkId { get; set; }
        public string sitename { get; set; }
        public string siteurl { get; set; }
        public string sitedesc { get; set; }
        public int Sort { get; set; }
        public int IsCheck { get; set; }
        public string Reason { get; set; }

        public DateTime CreatedTime { get; set; }
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 删除标识：（0：已删除，1：未删除）
        /// </summary>
        public int Enable { get; set; }
    }

    public enum FriendlyLinkIsCheck
    {
        None = 0,
        OK = 1,
        No = -1,
    }
}