using Blog.Models;
using Blog.Repository;
using Blog.Service;
using System;
using System.Collections.Generic;
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

        public ActionResult Index(int? pageId = 1)
        {
            ArticleListModel listModel = new ArticleListModel();
            listModel.PageIndex = Convert.ToInt32(pageId);
            listModel.PageSize = 3;

            IEnumerable<Article> list = _articleService.GetPaged(listModel);
            return View(list);
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
    }
}