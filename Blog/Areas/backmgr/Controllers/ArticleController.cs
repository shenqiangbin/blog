using Blog.Common;
using Blog.Models;
using Blog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Areas.backmgr.Controllers
{
    public class ArticleController : Controller
    {
        private ArticleService _articleService;

        public ArticleController(ArticleService articleService)
        {
            _articleService = articleService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            Article article = new Article();
            article.Title = "test";
            article.Content = "content";
            _articleService.Add(article);

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Add(string articleId, string title, string content)
        {
            try
            {
                AddValidate(articleId, title, content);

                Article article = new Article();
                article.Title = title;
                article.Content = content;
                _articleService.Add(article);

                return Json(new { code = 200, msg = "ok" });
            }
            catch (ValidateException ex)
            {
                return Json(new { code = ex.Code, msg = ex.Message });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = ex.Message });
            }
        }

        private void AddValidate(string articleId, string title, string content)
        {
            if (ValidateHelper.IsEmpty(title))
                throw new ValidateException(101, "请填写标题");
            if (ValidateHelper.IsOverLength(title, 50))
                throw new ValidateException(102, $"标题请在50字内");
            if (ValidateHelper.IsEmpty(content))
                throw new ValidateException(101, "请填写内容");
            //if (ValidateHelper.IsOverLength(content, 50))
            //    throw new ValidateException(102, $"标题请在50字内");
        }


        [HttpPost]
        public ActionResult Del(int? id)
        {
            try
            {
                if (!id.HasValue)
                    return Json(new { code = 400, msg = "id不能为空" });

                _articleService.Remove(id.Value);

                return Json(new { code = 200, msg = "ok" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = ex.Message });
            }
        }
    }
}