using Blog.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Areas.backmgr.Controllers
{
    public class LabelController : Controller
    {
        private LabelRepository _labelRepository;

        public LabelController(LabelRepository labelRepository)
        {
            _labelRepository = labelRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return View();
        }
    }
}