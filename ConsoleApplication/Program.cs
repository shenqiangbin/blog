﻿using Blog.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {


            MailHelper mailHelper = new MailHelper("1348907384@qq.com", "");
            mailHelper.sendEmail(new List<string>() { "1969858717@qq.com" }, "test", "<p>test content <a href='http://www.baidu.com'>123</a></p>", true);


            //GetTiKu();
            //HandleTiKu();
            //HandleTiKu2();
            //long number = Convert.ToInt64("sqlite-helper-summary");
            long number = MD5Helper.MD5ToNum($"sqlite-helper-summary");

            var md5 = MD5Helper.MD5Value("abc");
            var s = HashHelper.HashMd5("lyb123", md5);

            var sanitizer = new Ganss.XSS.HtmlSanitizer();
            sanitizer.AllowedTags.Add("iframe");
            sanitizer.AllowedTags.Add("link");
            sanitizer.AllowedTags.Add("script");
            sanitizer.AllowedAttributes.Add("frameborder");
            sanitizer.AllowedAttributes.Add("allowfullscreen");
            var ms = @"> 引入样式

`<link rel='stylesheet' href='css/editormd.min.css' />`

> 引入js

````
<script src='jquery.min.js'></script>
<script src='editormd.min.js'></script>
````
> HTML";
            var str = sanitizer.Sanitize(ms);

            HTMLToJS("");
            Console.WriteLine("ok");
            Console.ReadLine();
        }

        private static void HTMLToJS(string content)
        {

            content = @"
<div class ='replyBox'>
    <p>
        <span style='width:74px;display: inline-block;'><span class ='must'>*</span>昵称：</span>
        <input type='text' id='userName' value='' style='width:335px'/>
    </p>
    <p>
        <span style='width:74px;display: inline-block;'><span class ='must'>*</span>邮箱：</span>
        <input type='text' id='email' value='' style='width:335px'/>
    </p>
    <p>
        <span style='width:74px;display: inline-block;'>个人站点：</span>
        <input type='text' id='site' value='' style='width:335px'/>
    </p>
    <div id='content2' class ='commentcontent commentcontent2' contenteditable='true'></div>
    <img id='btn2' class ='emojiBtn' src='/Images/emoji.png' />
    <button id='publishBtn2' class ='publishBtn'>发表</button>
</div>
";
            
            string[] arr = content.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            var newArr = arr.Select(m => "\"" + m + "\"");
            string newVal = string.Join(" + \r\n", newArr);
        }

        private static void GetTiKu()
        {
            string items = @"
                劳动法,
                公务员法,
                劳动合同法,
                就业促进法,
                劳动争议调解仲裁法,
                社会保险法,
                国家勋章和国家荣誉称号法,
                劳动合同法实施条例,
                工伤保险条例,
                劳动保障监察条例,
                全国社会保障基金条例,
                社会保险费征缴暂行条例,
                女职工劳动保护特别规定,
                事业单位人事管理条例,
                职工带薪年休假条例,
                劳动人事争议仲裁组织规则,
                劳动人事争议仲裁办案规则,
                失业保险金申领发放办法,
                社会保险费申报缴纳管理规定,
                社会保险基金先行支付暂行办法,
                社会保险个人权益记录管理办法,
                社会保险费征缴监督检查办法,
                就业服务与就业管理规定,
                其他,
";
            List<string> itemList = items.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            string file = "d:/exam.txt";

            List<string> list = new List<string>();
            itemList.ForEach(m => { list.Add(m.Trim()); });

            foreach (string item in list)
            {
                if (string.IsNullOrEmpty(item))
                    continue;
                for (int i = 1; i < 10; i++)
                {
                    for (int j = 1; j < 10; j++)
                    {
                        var content = RegisterMetadata(item, i.ToString(), j.ToString());
                        if (content.Trim().StartsWith("<div class=\"page\">"))
                            break;
                        File.AppendAllText(file, content + "\r\n-----------------------------------------------------------------------------------------------");
                    }
                }
            }
        }

        private static string RegisterMetadata(string item, string questionType, string page)
        {

            string url = "http://saishi.cnki.net/Renshefagui/TikuPartial";
            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;

            request.Method = "Post";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            //获得用户名密码的Base64编码
            //request.Headers.Add("Authorization", "");
            //VFNJTkdIVUE6dHNpbmdodWFfbWRz
            var code = HttpUtility.UrlEncode(item);
            string body = $"TradeFirstGenreCode={code}&questionType={questionType}&page={page}";
            byte[] bytes = Encoding.UTF8.GetBytes(body);
            request.GetRequestStream().Write(bytes, 0, bytes.Length);

            try
            {
                WebResponse response = request.GetResponse();
                var stream = response.GetResponseStream();
                using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
                {
                    var content = sr.ReadToEnd();
                    return content;
                }
            }
            catch (WebException ex)
            {
                HttpWebResponse response = (HttpWebResponse)ex.Response;
                Console.WriteLine("Error code: {0}", response.StatusCode);
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    using (Stream data = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(data))
                        {
                            string text = reader.ReadToEnd();
                            Console.WriteLine(text);
                        }
                    }
                }
            }
            return "";
        }

        private static void HandleTiKu()
        {
            string file = @"d:/exam.txt";
            StreamReader reader = new StreamReader(File.OpenRead(file));

            StringBuilder builder = new StringBuilder();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line.Contains("、"))
                    builder.Append(line.Trim());
                if (line.Contains("正确答案"))
                {
                    Regex reg = new Regex("(?i)(?<=（)[^（]*(?=）)");//commend by danielinbiti  
                    MatchCollection mc = reg.Matches(line);
                    foreach (Match m in mc)
                    {
                        builder.Append(",");
                        builder.Append(m.Value);
                        builder.Append("\r\n");
                    }
                }
            }
            string s = builder.ToString();
        }

        private static void HandleTiKu2()
        {
            List<string> questions = new List<string>();
            List<string> answers = new List<string>();

            string content = File.ReadAllText("d:/exam.txt");
            var mc = GetMatch("<p class=\"qt\">", "</p>", content);
            foreach (Match m in mc)
            {
                var index = m.Value.IndexOf("、");
                var s = m.Value.Substring(index + 1).Trim();
                questions.Add(s);
            }

            var mc2 = GetMatch("<div class=\"result\">", "</div>", content);
            foreach (Match m in mc2)
            {
                var s = m.Value.Trim();
                answers.Add(s);
                //if (!string.IsNullOrEmpty(s) && Regex.IsMatch(s, "^[A-Z]+$"))
                //{
                //    answers.Add(s);
                //}

            }
            var a = questions;
        }

        private static MatchCollection GetMatch(string qian, string hou, string content)
        {
            Regex reg = new Regex(string.Format("(?i)(?<={0})[^{0}]*(?={1})", qian, hou));//commend by danielinbiti  
            MatchCollection mc = reg.Matches(content);
            return mc;
        }
    }
}
