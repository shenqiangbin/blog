using Blog.Common;
using Blog.Models;
using Blog.Service;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class CommentController : Controller
    {
        private CommentService _commentService;

        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }

        public ActionResult Index(string articleId)
        {
            ViewBag.articleId = articleId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Add(string articleId, string content)
        {
            try
            {
                AddValidate(articleId, content);

                _commentService.Add(new Comment { ArticleId = Convert.ToInt32(articleId), UserName = "游客", Content = content });

                return Json(new { code = 200, msg = "ok" });
            }
            catch (Exception ex)
            {
                LogService.Instance.AddAsync(Models.Level.Error, ex);
                return Json(new { code = 500, msg = ex.Message });
            }
        }

        private void AddValidate(string articleId, string content)
        {
            if (ValidateHelper.IsEmpty(articleId))
                throw new ValidateException(101, "articleId不能为空");
            if (ValidateHelper.IsOverLength(articleId, 50))
                throw new ValidateException(102, $"articleId请在50字内");
            if (ValidateHelper.IsEmpty(content))
                throw new ValidateException(101, "请填写内容");
            if (ValidateHelper.IsOverLength(content, 500))
                throw new ValidateException(102, $"内容请少于500字");
        }

        public ActionResult List(string articleId, int? page = 1)
        {
            try
            {
                if (string.IsNullOrEmpty(articleId))
                    return Json(new { code = 401, msg = "articleId不能为空" });

                CommentListQuery listModel = new CommentListQuery();
                listModel.PageIndex = Convert.ToInt32(page);
                listModel.PageSize = 10;

                CommentListModelResult result = _commentService.GetPaged(listModel);
                //StaticPagedList<Comment> pageList = new StaticPagedList<Comment>(result.List, listModel.PageIndex, listModel.PageSize, result.TotalCount);                
                return Json(new { code = 200, data = result.List }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogService.Instance.AddAsync(Level.Error, ex);
                return Json(new { code = 500, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}