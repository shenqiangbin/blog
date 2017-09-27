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
        private ArticleLabelService _articleLabelService;

        public LabelController(LabelService labelService, ArticleLabelService articleLabelService)
        {
            _labelService = labelService;
            _articleLabelService = articleLabelService;
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
                LogService.Instance.AddAsync(Models.Level.Error, ex);
                return Json(new { code = 500, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetLablesByArticle(int articleId)
        {
            try
            {               
                var models = _labelService.GetLablesByArticle(articleId);
                return Json(new { code = 200, data = models }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogService.Instance.AddAsync(Models.Level.Error, ex);
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