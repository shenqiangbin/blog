using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Areas.backmgr.Controllers
{
    public class HomeController : Controller
    {
        // GET: backmgr/Home
        public ActionResult Index()
        {
            //return Content("backmgr");
           return View();
        }
    }
}