using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using Blog.App_Start;
using System.Web.Optimization;
using Blog.Common;

namespace Blog
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            AutofacHelper.Inject();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthorizeRequest(object sender, System.EventArgs e)
        {
            SessionHelper.SetUser();
        }
    }
}