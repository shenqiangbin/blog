using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.UI;

namespace Blog.Filters
{
    public class StaticHtmlAttribute : ActionFilterAttribute
    {
        //用于保存渲染后的html文本
        static StringBuilder sb;
        //这几个Writer照着写就行了
        static StringWriter sw;
        static HtmlTextWriter hw;
        static TextWriter tw;
        //自定义的静态页面的后缀名
        static string ext = ".html";
        //静态页面的绝对路径(包括后缀名)
        string fileName = null;
        ///静态页面的绝对路径(不包括后缀名)
        static string path = null;
        //静态文件是否存在
        bool FileExists = false;

        /// <summary>
        /// Action执行前，判断当前页面是否已经被静态化（Views路径下是否存在html文件）
        /// 如果存在静态文件则直接设置filterContext的result，即返回html作为结果，而不执行Action中代码
        /// 如果不存在静态页面文件，则不设置filterContext的result，此时将会执行Action中的代码
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //根据controller和action信息
            string controller = filterContext.RouteData.Values["controller"].ToString();
            string action = filterContext.RouteData.Values["action"].ToString();
            string urlTitle = filterContext.RouteData.Values["urlTitle"].ToString();
            // object id = null;
            //路由中是否包含可选参数id，如果有，则在文件名也要体现
            //if (!filterContext.RouteData.Values.TryGetValue("urlTitle", out id))
            //{
            //    path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Views", controller, action);
            //    fileName = string.Format("{0}{1}", path, ext);
            //}
            //else
            //{
            //    path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Views", controller, action);
            //    fileName = string.Format("{0}{1}{2}", path, id.ToString(), ext);
            //}

            path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "html", controller + "-" + action);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            fileName = Path.Combine(path, urlTitle + ext);


            //拼装后缀名

            FileExists = File.Exists(fileName);
            //如果文件存在，直接返回结果
            if (FileExists)
            {
                filterContext.Result = new FileContentResult(File.ReadAllBytes(fileName), "text/html; charset=utf-8");
            }
        }
        /// <summary>
        /// 执行完Action后，但渲染页面前执行此处
        /// 渲染页面的意思是将cshtml中的后台代码，翻译为前台代码
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {

            if (!FileExists)
            {
                //保存html
                sb = new StringBuilder();
                //两个writer
                sw = new StringWriter(sb);
                hw = new HtmlTextWriter(sw);
                //记住Response中原本输出流，用于返回本次请求的html，与下一句配合使用
                //在渲染结束后，向tw内写入html内容
                tw = filterContext.RequestContext.HttpContext.Response.Output;
                //过滤器自己输出流，用于获取渲染后的html内容
                filterContext.RequestContext.HttpContext.Response.Output = hw;
            }

        }
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            //如果是静态文件不存在
            if (!FileExists)
            {
                //获取渲染后的html文本
                string res = sb.ToString();
                //将文本写入到静态文件中
                new Action(() => File.WriteAllText(fileName, res)).BeginInvoke(null, null);
                //向Response的输出流中写入本次请求的html
                tw.Write(sb.ToString());
            }
        }
    }

}