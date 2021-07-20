using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace Blog.Common
{
    public class MailHelper
    {
        private string Email;
        private string Pwd;

        public MailHelper(string email, string pwd)
        {
            this.Email = email;
            this.Pwd = pwd;
        }

        public void sendEmail(List<string> toList, string subject, string body, bool isHTML)
        {
            SmtpClient mailClient = new SmtpClient("smtp.qq.com");
            mailClient.EnableSsl = true;
            //Credentials登陆SMTP服务器的身份验证.
            mailClient.Credentials = new NetworkCredential(this.Email, this.Pwd);
            mailClient.Port = 587;
            mailClient.EnableSsl = true;
            //test@qq.com发件人地址、test@tom.com收件人地址

            MailMessage message = new MailMessage();
            message.From = new MailAddress(Email);
            //message.To.Add(list);
            toList.ForEach(m => message.To.Add(m));
            message.IsBodyHtml = isHTML;
            message.BodyEncoding = Encoding.UTF8;

            // message.Bcc.Add(new MailAddress("tst@qq.com")); //可以添加多个收件人
            message.Body = body;//邮件内容
            message.Subject = subject;//邮件主题
                                      //Attachment 附件
                                      //Attachment att = new Attachment(@"D:/test.mp3");
                                      //message.Attachments.Add(att);//添加附件



            //mailClient.Send(message);
            object userState = message;
            mailClient.SendAsync(message, userState);
            //mailClient.SendMailAsync(message);
            //mailClient.Send(message); 
        }
    }
}