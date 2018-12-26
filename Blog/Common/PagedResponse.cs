using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Common
{
    public class PagedResponse<T>
    {
        public IList<T> List;
        public int CurrentPage;
        public int TotalPage;
        public long TotalCount;
        public int PageSize;
    }
}