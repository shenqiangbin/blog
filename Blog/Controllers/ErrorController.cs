using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Eoor
        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult HasError()
        {
            return View();
        }
    }
}