using Blog.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
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
    }
}