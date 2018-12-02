using Blog.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Blog.Controllers
{
    public class FileController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Upload()
        {
            HttpFileCollection files = HttpContext.Current.Request.Files;
            HttpPostedFile file = files[0];

            if (file == null)
            {
                return Ok(new { success = 0, message = "没有文件" });
            }
            else if (!CheckFileType(file))
            {
                return Ok(new { success = 0, message = "文件类型不对" });
            }
            else if (!CheckFileSize(file))
            {
                return Ok(new { success = 0, message = "文件大于请小于20M" });
            }

            string extension = file.FileName.Substring(file.FileName.LastIndexOf("."));
            string theFile = Path.GetFileName(MD5Helper.MD5Value(file.FileName))+extension;

            string folderPath = HttpContext.Current.Request.MapPath("~/Upload");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = Path.Combine(folderPath, theFile);
            try
            {
                file.SaveAs(fileName);
                string path = "/upload/" + theFile;
                return Ok(new { success = 1, message = "", url = path });
            }
            catch (Exception ex)
            {
                return Ok(new { success = 1, message = ex.Message });
            }
        }

        private bool CheckFileType(HttpPostedFile file)
        {
            string extension = file.FileName.Substring(file.FileName.LastIndexOf("."));
            List<string> extens = new List<string>() { ".jpg", ".jpeg", ".gif", ".png", ".bmp" };
            return extens.Contains(extension);
        }

        private bool CheckFileSize(HttpPostedFile file)
        {
            var size = file.ContentLength / 1024 / 1024;
            if (size > 200) //如果大于20M
                return false;
            else
                return true;
        }
    }
}