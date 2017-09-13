using Blog.Models;
using Blog.Service;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Areas.backmgr.Controllers
{
    public class HomeController : Controller
    {
        private ArticleService _articleService;

        public HomeController(ArticleService articleService)
        {
            _articleService = articleService;
        }

        public ActionResult Index(int? page = 1)
        {
            ArticleListQuery query = new ArticleListQuery();
            query.PageIndex = Convert.ToInt32(page);
            query.PageSize = 10;
            query.PublishStatus = PublishStatus.All;

            ArticleListModelResult result = _articleService.GetPaged(query);
            var pageList = new StaticPagedList<Article>(result.List, query.PageIndex, query.PageSize, result.TotalCount);

            return View(pageList);
        }
    }
}