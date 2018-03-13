using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Common
{
    public class XSSHelper
    {
        public static string Sanitize(string html)
        {
            var sanitizer = new Ganss.XSS.HtmlSanitizer();
            sanitizer.AllowedTags.Add("iframe");
            sanitizer.AllowedAttributes.Add("frameborder");
            sanitizer.AllowedAttributes.Add("allowfullscreen");
            return sanitizer.Sanitize(html);
        }
    }
}