using Blog.Common;
using Blog.Models;
using Blog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class FriendlylinkController : Controller
    {
        private FriendlyLinkService friendlyLinkService;

        public FriendlylinkController(FriendlyLinkService friendlyLinkService)
        {
            this.friendlyLinkService = friendlyLinkService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Apply()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Apply(string sitename, string siteurl, string sitedesc, string validateCode)
        {
            try
            {
                ValidateApply(sitename, siteurl, sitedesc, validateCode);
                FriendlyLink model = new FriendlyLink
                {
                    sitename = sitename,
                    siteurl = siteurl,
                    sitedesc = sitedesc
                };
                friendlyLinkService.Add(model);
                return Json(new { code = 200, msg = "新增成功" });
            }
            catch (ValidateException ex)
            {
                return Json(new { code = ex.Code, msg = ex.Message });
            }
            catch (Exception ex)
            {
                LogService.Instance.AddAsync(Models.Level.Error, ex);
                return Json(new { code = 500, msg = "服务器异常，请稍后重试" });
            }
        }

        private void ValidateApply(string sitename, string siteurl, string sitedesc, string validateCode)
        {

            if (string.IsNullOrEmpty(sitename))
                throw new ValidateException(400, "站点名称不能为空");
            if (sitename.Length > 50)
                throw new ValidateException(400, "站点名称请小于50字");

            if (string.IsNullOrEmpty(siteurl))
                throw new ValidateException(401, "站点地址不能为空");
            if (siteurl.Length > 150)
                throw new ValidateException(401, "站点地址请小于150字");

            if (string.IsNullOrEmpty(sitedesc))
                throw new ValidateException(402, "站点描述不能为空");
            if (sitedesc.Length > 150)
                throw new ValidateException(401, "站点描述请小于150字");

            if (Session["Code"] != null)
            {
                if (!string.Equals(validateCode, Session["Code"].ToString(), StringComparison.CurrentCultureIgnoreCase))
                    throw new ValidateException(402, "验证码不正确");
            }
        }
    }
}