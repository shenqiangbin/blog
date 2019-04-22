using Blog.Common;
using Blog.Models;
using Blog.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Service
{
    public class CategoryService
    {
        private CategoryRepository _CategoryRepository;

        public CategoryService(CategoryRepository CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }

        public int Add(string name,int parentCategoryId,int sort)
        {
            if (_CategoryRepository.SelectByName(name) != null)
                throw new ValidateException(301, "分类已存在");

            var model = new Category();
            model.Name = name;
            model.ParentCategoryId = parentCategoryId;
            model.Sort = sort;
            return Add(model);
        }

        public int Add(Category model)
        {
            if (ContextUser.IsLogined)
                model.CreateUser = ContextUser.Email;

            var time = DateTime.Now;

            model.CreateTime = time;
            model.UpdateTime = time;
            model.Enable = 1;

            var id = _CategoryRepository.Add(model);
            return id;
        }

        public int Update(Category model)
        {
            var dbModel = _CategoryRepository.SelectByName(model.Name);
            if (dbModel != null && dbModel.CategoryId != model.CategoryId)
                throw new ValidateException(301, "标签已存在");

            model.UpdateTime = DateTime.Now;
            return _CategoryRepository.Update(model);
        }

        public List<Category> GetAll()
        {
            return _CategoryRepository.GetAll();
        }

    }
}