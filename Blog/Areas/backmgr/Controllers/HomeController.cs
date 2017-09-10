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
            ArticleListModel listModel = new ArticleListModel();
            listModel.PageIndex = Convert.ToInt32(page);
            listModel.PageSize = 10;

            ArticleListModelResult result = _articleService.GetPaged(listModel);
            var pageList = new StaticPagedList<Article>(result.List, listModel.PageIndex, listModel.PageSize, result.TotalCount);

            return View(pageList);
        }
    }
}