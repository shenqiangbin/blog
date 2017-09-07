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

        public ActionResult Index()
        {
            ArticleRepository repository = new ArticleRepository();
            repository.Add(new Models.Article
            {
                Title = "title",
                Content = "content",
                DisplayCreatedTime = DateTime.Now,
                CreatedTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                Enable = 1
                
            });

            return View();
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