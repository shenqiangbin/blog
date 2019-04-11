using Blog.Common;
using Blog.Models;
using Blog.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Service
{
    public class FriendlyLinkService
    {
        private FriendlyLinkRepository _FriendlyLinkRepository;

        public FriendlyLinkService(FriendlyLinkRepository FriendlyLinkRepository)
        {
            _FriendlyLinkRepository = FriendlyLinkRepository;
        }

        public int Add(FriendlyLink model)
        {
            model.IsCheck = (int)FriendlyLinkIsCheck.None;

            var time = DateTime.Now;
            model.CreatedTime = time;
            model.UpdateTime = time;
            model.Enable = 1;

            var id = _FriendlyLinkRepository.Add(model);
            return id;
        }

        //public int Update(FriendlyLink model)
        //{
        //    model.UpdateTime = DateTime.Now;
        //    model.UrlTitleNum = MD5Helper.MD5ToNum(model.UrlTitle).ToString();

        //    return _FriendlyLinkRepository.Update(model);
        //}

        //public FriendlyLink GetById(string FriendlyLinkId)
        //{
        //    return _FriendlyLinkRepository.GetById(FriendlyLinkId);
        //}

        //public FriendlyLink GetByUrlTitle(string urlTitle)
        //{
        //    var list = _FriendlyLinkRepository.GetByColumn("urlTitle", urlTitle);
        //    if (list.Count > 1)
        //        throw new ValidateException(409, "urltitle重复");
        //    else
        //        return list.FirstOrDefault();
        //}

        //public FriendlyLink GetByUrlTitleNum(string urlTitleNum)
        //{
        //    var list = _FriendlyLinkRepository.GetByColumn("urlTitleNum", urlTitleNum);
        //    if (list.Count > 1)
        //        throw new ValidateException(409, "urlTitleNum重复");
        //    else
        //        return list.FirstOrDefault();
        //}

        //public FriendlyLinkListModelResult GetPaged(FriendlyLinkListQuery listModel)
        //{
        //    return _FriendlyLinkRepository.GetPaged(listModel);
        //}

        //public void Remove(int id)
        //{
        //    _FriendlyLinkRepository.Remove(id);
        //}
        public IEnumerable<FriendlyLink> GetAllChecked()
        {
            var models = this.GetAll();
            if (models != null)
                return models.Where(m => m.IsCheck == (int)FriendlyLinkIsCheck.OK);
            else
                return new List<FriendlyLink>();
        }

        public List<FriendlyLink> GetAll()
        {
            var models = _FriendlyLinkRepository.GetAll();
            return models;
        }
    }
}