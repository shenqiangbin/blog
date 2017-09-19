﻿using Blog.Common;
using Blog.Filters;
using Blog.Models;
using Blog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Areas.backmgr.Controllers
{
    [UserAuthorize]
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

        public ActionResult Add(string id)
        {
            Article model = new Article();
            ViewBag.IsNew = true;

            if (!string.IsNullOrEmpty(id))
            {
                model = _articleService.GetById(id);
                ViewBag.IsNew = false;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Add(string articleId, string title, string content)
        {
            try
            {
                AddValidate(articleId, title, content);

                //新增
                if (string.IsNullOrEmpty(articleId))
                {
                    Article article = new Article();
                    article.Title = title;
                    article.Content = content;
                    articleId = _articleService.Add(article).ToString();
                }
                else //编辑
                {
                    var model = _articleService.GetById(articleId);
                    model.Title = title;
                    model.Content = content;
                    _articleService.Update(model);
                }

                return Json(new { code = 200, msg = "ok", id = articleId });
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
        [ValidateAntiForgeryToken]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Publish(int? id)
        {
            try
            {
                if (!id.HasValue)
                    return Json(new { code = 400, msg = "id不能为空" });

                _articleService.Publish(id.Value);

                return Json(new { code = 200, msg = "ok" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = ex.Message });
            }
        }
    }
}