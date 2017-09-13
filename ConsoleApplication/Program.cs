using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
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
                        File.AppendAllText(file,content + "\r\n-----------------------------------------------------------------------------------------------");
                    }
                }
            }
            Console.WriteLine("ok");
            Console.ReadLine();
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
    }
}
