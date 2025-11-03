using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;

namespace Temiang.Avicenna.Common
{
    public class Mail
    {
        private class EmailData
        {
            public string ToEmailAddress { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
        }
        public static void SendMail(string toAddress, string subject, string body)
        {
            if (string.IsNullOrWhiteSpace(toAddress)) return;

            try
            {
                // https://myaccount.google.com/lesssecureapps?pli=1 <- harus dienablekan
                //string fromAddress = "AvicennaHis.SCI@gmail.com";
                //string fromPassword = "sciadmin88";
                //toAddress ->  avicennahis.sci.logerror@gmail.com -> sciadmin88

                //Seting baru supaya bisa menggunakan gmail
                //https://support.google.com/mail/answer/185833?hl=en
                //Create & use app passwords
                //Important: To create an app password, you need 2-Step Verification on your Google Account.
                //If you use 2-Step-Verification and get a "password incorrect" error when you sign in, you can try to use an app password.
                //1. Go to your Google Account. (https://myaccount.google.com/)
                //2. Select Security.
                //3. Under "Signing in to Google," select 2-Step Verification.
                //  3.1 At the bottom of the page, select App passwords.
                //  3.2 Enter a name that helps you remember where you’ll use the app password.
                //  3.3 Pilih Email dan Komputer Windows
                //  3.4 Select Generate.
                //4. Save ke parameter EmailPassword hasil generate passwordnya

                var fromAddress = AppParameter.GetParameterValue(AppParameter.ParameterItem.EmailAddress);
                if (string.IsNullOrWhiteSpace(fromAddress)) return;

                var fromPassword = AppParameter.GetParameterValue(AppParameter.ParameterItem.EmailPassword);
                var host = AppParameter.GetParameterValue(AppParameter.ParameterItem.EmailHost);
                var port = AppParameter.GetParameterValue(AppParameter.ParameterItem.EmailPort).ToInt();
                // smtp settings
                var smtp = new System.Net.Mail.SmtpClient();
                {
                    smtp.Host = string.IsNullOrEmpty(host) ? "smtp.gmail.com" : host;
                    smtp.Port = port == 0 ? 587 : port;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                    smtp.Timeout = 20000;
                }
                // Passing values to smtp object
                smtp.Send(fromAddress, toAddress, subject, body);
            }
            catch (Exception ex)
            {
            }
        }

        private static void SendMail(object emailData)
        {
            var data = (EmailData)emailData;
            SendMail(data.ToEmailAddress, data.Subject, data.Body);
        }

        public static void SendMailUseOtherThread(string toAddress, string subject, string body)
        {
            var thread = new Thread(SendMail);
            var emailData = new EmailData()
            {
                ToEmailAddress = toAddress,
                Subject = subject,
                Body = body
            };
            thread.Start(emailData);
        }
    }
}
