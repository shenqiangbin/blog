using Blog.Filters;
using Blog.Repository;
using Blog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Areas.backmgr.Controllers
{
    [UserAuthorize]
    public class LabelController : Controller
    {
        private LabelService _labelService;

        public LabelController(LabelService labelService)
        {
            _labelService = labelService;
        }

        public JsonResult GetAll()
        {
            try
            {
                var models = _labelService.GetAll();
                return Json(new { code = 200, data = models }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
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