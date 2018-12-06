using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class UtilController : Controller
    {
        // GET: Util
        public ActionResult HTML2JS()
        {
            ViewBag.HTML = @"
<div class='replyBox'>
    <p>
        <span style=""width:74px;display: inline-block;""><span class =""must"">*</span>昵称：</span>
        <input type=""text"" id=""userName"" value="""" style=""width:335px""/>
    </p>
    <p>
        <span style=""width:74px;display: inline-block;""><span class =""must"">*</span>邮箱：</span>
        <input type=""text"" id=""email"" value="""" style=""width:335px""/>
    </p>
    <p>
        <span style=""width:74px;display: inline-block;"">个人站点：</span>
        <input type=""text"" id=""site"" value="""" style=""width:335px""/>
    </p>
    <div id='content2' class ='commentcontent commentcontent2' contenteditable='true'></div>
    <img id='btn2' class ='emojiBtn' src='/Images/emoji.png' />
    <button id='publishBtn2' class ='publishBtn'>发表</button>
</div>
";

            return View();
        }
    }
}