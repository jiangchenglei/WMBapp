using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Bill.Utility.Helper
{
    public class SendEmailHelper
    {
        public static int SmtpPort = 25;
        public static string SmtpHost = "smtp.exmail.qq.com";
        public static string UserName = "buyerservice@osell.com";
        public static string Password = "Osell123";
        public static string MasterEmail = "buyerservice@osell.com";
        public static string MasterName = "buyerservice";

        /// <summary> 
        /// 发送邮件 
        /// </summary> 
        /// <param name="toEmail">收件地址</param> 
        /// <param name="toEmailName">收件人名字</param> 
        /// <param name="subject">邮件主题</param> 
        /// <param name="body">邮件内容</param> 
        public static int Send(string toEmail, string toEmailName, string subject, string body)
        {
            try
            {
                if (toEmail.Contains("@test.com"))//过滤掉测试邮件 
                    return 0;
                if (SmtpPort <= 0)
                    SmtpPort = 25;
                SmtpClient client = new SmtpClient(SmtpHost, SmtpPort);
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(UserName, Password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = false;
                MailAddress addressFrom = new MailAddress(MasterEmail, MasterName);
                MailAddress addressTo = new MailAddress(toEmail, toEmailName);

                MailMessage message = new MailMessage(addressFrom, addressTo);
                message.Sender = addressFrom;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                message.Subject = subject;
                message.Body = body;
                client.Send(message);
                message.Dispose();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
