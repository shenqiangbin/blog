﻿using Blog.Filters;
using Blog.Models;
using Blog.Repository;
using Blog.Service;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private ArticleService _articleService;

        public HomeController(ArticleService articleService)
        {
            _articleService = articleService;
        }

        public ActionResult Index(int? page = 1, string search = "")
        {
            ViewBag.SearchValue = search;

            ArticleListQuery listModel = new ArticleListQuery();
            listModel.PageIndex = Convert.ToInt32(page);
            listModel.PageSize = 10;
            listModel.Search = search;

            ArticleListModelResult result = _articleService.GetPaged(listModel);
            var pageList = new StaticPagedList<Article>(result.List, listModel.PageIndex, listModel.PageSize, result.TotalCount);

            return View(pageList);
        }

        public ActionResult Detail(string id)
        {
            Article model = null;

            if (!string.IsNullOrEmpty(id))
            {
                model = _articleService.GetById(id);
            }

            return View(model);
        }

        [StaticHtml]
        public ActionResult ViewArticle(string urlTitle)
        {
            if (string.IsNullOrEmpty(urlTitle))
                return Content("参数呢?你吃了?");
            Article model = _articleService.GetByUrlTitle(urlTitle);            
            return View("Detail",model);
        }

        public ActionResult Summary()
        {
            return View();
        }
    }
}