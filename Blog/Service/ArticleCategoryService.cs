using Blog.Common;
using Blog.Models;
using Blog.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Service
{
    public class ArticleCategoryService
    {
        private ArticleCategoryRepository _articleCategoryRepository;

        public ArticleCategoryService(ArticleCategoryRepository articleCategoryRepository)
        {
            _articleCategoryRepository = articleCategoryRepository;
        }

        public int Add(int articleId,int lableId)
        {
            var model = new ArticleCategory();
            model.ArticleId = articleId;
            model.CategoryId = lableId;

            if (ContextUser.IsLogined)
                model.CreateUser = ContextUser.Email;

            var time = DateTime.Now;

            model.CreateTime = time;
            model.UpdateTime = time;
            model.Enable = 1;

            var id = _articleCategoryRepository.Add(model);
            return id;
        }

        public void RemoveByArticleId(int articleId)
        {
            _articleCategoryRepository.RemoveByArticleId(articleId);
        }
    }
}