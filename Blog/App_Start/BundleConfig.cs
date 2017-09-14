using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Blog.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));     

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/mcss").Include("~/Content/msite.css"));
            bundles.Add(new StyleBundle("~/Content/index").Include("~/Content/index.css","~/Content/PagedList.css"));
            bundles.Add(new StyleBundle("~/Content/backindex").Include("~/Content/backindex.css", "~/Content/PagedList.css"));


            bundles.Add(new ScriptBundle("~/Scripts/ueditor/js").Include(
                        "~/Scripts/ueditor/ueditor.config.js",
                        "~/Scripts/ueditor/ueditor.all.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/Scripts/ueditor/lang/zh-cn/zh").Include(
                        "~/Scripts/ueditor/lang/zh-cn/zh-cn.js"
                        ));

        }
    }
}