using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Blog.Filters
{
    public class ExcepitonFilter : IExceptionFilter
    {
        public bool AllowMultiple
        {
            get; set;
        }

        public void OnException(ExceptionContext filterContext)
        {
            try
            {
                Blog.Service.LogService.Instance.AddAsync(Models.Level.Error, filterContext.Exception);
            }
            catch (Exception)
            { }
        }
    }

}