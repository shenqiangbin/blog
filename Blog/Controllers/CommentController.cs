﻿using Blog.Common;
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
        //[ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Add(string articleId, string userName, string email, string site, string content, string commentId)
        {
            try
            {
                AddValidate(articleId, content, userName, email, site);

                int id = _commentService.Add(
                    new Comment
                    {
                        ArticleId = Convert.ToInt32(articleId),
                        UserName = string.IsNullOrEmpty(userName) ? "游客" : userName,
                        Email = email,
                        Site = site,
                        Content = content,
                        ParentId = Convert.ToInt32(commentId)
                    }
                );

                return Json(new { code = 200, msg = "ok", data = _commentService.GetById(id.ToString()) });
            }
            catch (Exception ex)
            {
                LogService.Instance.AddAsync(Models.Level.Error, ex);
                return Json(new { code = 500, msg = ex.Message });
            }
        }

        private void AddValidate(string articleId, string content, string userName, string email, string site)
        {
            if (ValidateHelper.IsEmpty(articleId))
                throw new ValidateException(101, "articleId不能为空");
            if (ValidateHelper.IsOverLength(articleId, 50))
                throw new ValidateException(102, $"articleId请在50字内");

            if (ValidateHelper.IsEmpty(userName))
                throw new ValidateException(101, "请填写昵称");
            if (ValidateHelper.IsOverLength(userName, 10))
                throw new ValidateException(102, $"昵称请少于10个字");

            if (ValidateHelper.IsEmpty(email))
                throw new ValidateException(101, "请填写邮箱");
            if (ValidateHelper.IsOverLength(email, 50))
                throw new ValidateException(102, $"昵称请少于50个字");

            if (ValidateHelper.IsEmpty(content))
                throw new ValidateException(101, "请填写内容");
            if (ValidateHelper.IsOverLength(content, 500))
                throw new ValidateException(102, $"内容请少于500字");

            if (ValidateHelper.IsEmpty(site)) {
            }
            else
            {
                if(ValidateHelper.IsOverLength(site, 50))
                    throw new ValidateException(102, $"个人站点请少于50字");
            }
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
                listModel.ArticleId = articleId;

                CommentListModelResult result = _commentService.GetPaged(listModel);
                StaticPagedList<Comment> pageList = new StaticPagedList<Comment>(result.List, listModel.PageIndex, listModel.PageSize, result.TotalCount);

                GetChildren(result.List.ToList());

                return Json(new { code = 200, data = result.List, pageNumber = pageList.PageNumber, pageCount = pageList.PageCount }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogService.Instance.AddAsync(Level.Error, ex);
                return Json(new { code = 500, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        private void GetChildren(List<Comment> comments)
        {
            List<int> ids = comments.Select(m => m.CommentId).ToList();
            List<Comment> children = _commentService.GetByParentIds(ids);

            foreach (var comment in comments)
            {
                comment.Children = children.Where(m => m.ParentId == comment.CommentId);
            }
        }
    }
}