using Blog.Models;
using Blog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Areas.backmgr.Controllers
{
    public class ArticelCategoryController : Controller
    {
        private CategoryService _categoryService;

        public ArticelCategoryController(CategoryService _categoryService)
        {
            this._categoryService = _categoryService;
        }

        // GET: backmgr/ArticelCategory
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(string name)
        {
            try
            {
                //验证name 略

                Category category = new Category();
                category.Name = name;
                category.ParentCategoryId = 0;

                var categoryId = _categoryService.Add(category);

                return Json(new { code = 200, msg = "ok", data = categoryId });
            }
            catch (Exception ex)
            {
                LogService.Instance.AddAsync(Level.Error, ex);
                return Json(new { code = 500, msg = "error" });
            }
        }


        [HttpGet]
        public ActionResult GetCategory()
        {
            try
            {
                List<Category> list = _categoryService.GetAll();
                return Json(new { code = 200, msg = "ok", data = list }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogService.Instance.AddAsync(Level.Error, ex);
                return Json(new { code = 500, msg = "error" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult SaveCategorySort(List<Category> list)
        {
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Sort = i;
                }

                _categoryService.UpdateSort(list); ;

                return Json(new { code = 200, msg = "ok" });
            }
            catch (Exception ex)
            {
                LogService.Instance.AddAsync(Level.Error, ex);
                return Json(new { code = 500, msg = "error" });
            }
        }
    }
}