using Blog.Common;
using Blog.Models;
using Blog.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Service
{
    public class ArticleService
    {
        private ArticleRepository _articleRepository;

        public ArticleService(ArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public int Add(Article model)
        {
            model.ContentLevel = (int)ContentLevel.Common;
            model.PublishStatus = (int)PublishStatus.Not;

            var time = DateTime.Now;

            model.DisplayCreatedTime = time;

            if (ContextUser.IsLogined)
                model.CreateUser = ContextUser.Email;

            model.CreatedTime = time;
            model.UpdateTime = time;
            model.Enable = 1;

            var id = _articleRepository.Add(model);
            return id;
        }

        public int Update(Article model)
        {
            model.UpdateTime = DateTime.Now;
            return _articleRepository.Update(model);
        }

        public Article GetById(string articleId)
        {
            return _articleRepository.GetById(articleId);
        }

        public ArticleListModelResult GetPaged(ArticleListQuery listModel)
        {
            return _articleRepository.GetPaged(listModel);
        }

        public void Remove(int id)
        {
            _articleRepository.Remove(id);
        }

        public void Publish(int id)
        {
            var model = _articleRepository.GetById(id.ToString());
            if (model.PublishStatus == (int)PublishStatus.Published)
                model.PublishStatus = (int)PublishStatus.Not;
            else if (model.PublishStatus == (int)PublishStatus.Not)
                model.PublishStatus = (int)PublishStatus.Published;

            model.UpdateTime = DateTime.Now;

            _articleRepository.Update(model);
        }
    }
}