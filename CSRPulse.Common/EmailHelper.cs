//using EASendMail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSRPulse.Common
{
    public class EmailHelper : IDisposable
    {
        /// <summary>
        /// Send Email Method 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        /// 



        public static async Task<bool> SendEmail(EmailMessage message)
        {

            using (SmtpClient client = new SmtpClient(message.SmtpClientHost, message.SmtpPort))
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(message.From,message.FriendlyName);
                mailMessage.BodyEncoding = Encoding.UTF8;

                foreach (var toAddress in message.To.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mailMessage.To.Add(toAddress);
                }
                if (!String.IsNullOrEmpty(message.CC))
                {
                    foreach (var ccAddress in message.CC.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        mailMessage.CC.Add(ccAddress);
                    }
                }

                if (!String.IsNullOrEmpty(message.Bcc))
                {
                    foreach (var bccAddress in message.Bcc.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        mailMessage.Bcc.Add(bccAddress);
                    }
                }
                foreach (var attach in message.Attachments)
                {
                    Attachment attachment;
                    if (attach.Content != null)
                    {
                        attachment = AddAttachment(attach.Content, attach.FileName);
                    }
                    else
                    {
                        attachment = AddAttachment(attach.FileName);
                    }
                    mailMessage.Attachments.Add(attachment);
                }
                mailMessage.Body = message.Body;
                mailMessage.Subject = message.Subject;
                mailMessage.IsBodyHtml = message.HTMLView;
                client.EnableSsl = true;
                mailMessage.Priority = message.MailPriority;

                #region Set Embedded Content
                if (message.EmbeddedContent != null)
                {
                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(message.Body, Encoding.UTF8, MediaTypeNames.Text.Html);
                    AlternateView plainView = AlternateView.CreateAlternateViewFromString(Regex.Replace(message.Body, "<[^>]+?>", string.Empty), Encoding.UTF8, MediaTypeNames.Text.Plain);
                    string mediaType = MediaTypeNames.Image.Jpeg;
                    foreach (var ec in message.EmbeddedContent)
                    {
                        LinkedResource img = new LinkedResource(ec.Path, mediaType);
                        img.ContentId = ec.ContentID;
                        img.ContentType.MediaType = mediaType;
                        img.TransferEncoding = TransferEncoding.Base64;
                        img.ContentType.Name = img.ContentId;
                        img.ContentLink = new Uri("cid:" + img.ContentId);
                        htmlView.LinkedResources.Add(img);
                    }
                    mailMessage.AlternateViews.Add(plainView);
                    mailMessage.AlternateViews.Add(htmlView);
                }
                #endregion

                client.Credentials = new NetworkCredential(message.UserName, message.Password);

               

                try
                {
                    await client.SendMailAsync(mailMessage);                 
                    return true;
                }

                catch (Exception)
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// EMAILTEST METHOD --
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        //public static bool SendEmail(EmailMessage message)
        //{          
        //    using (MailMessage email = new MailMessage())
        //    {
        //        email.From = new MailAddress(message.From, message.FriendlyName);
        //        foreach (var toAddress in message.To.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
        //        {
        //            email.To.Add(toAddress);
        //        }

        //        if (!String.IsNullOrEmpty(message.CC))
        //        {
        //            foreach (var ccAddress in message.CC.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
        //            {
        //                email.CC.Add(ccAddress);
        //            }
        //        }

        //        if (!String.IsNullOrEmpty(message.Bcc))
        //        {
        //            foreach (var bccAddress in message.Bcc.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
        //            {
        //                email.Bcc.Add(bccAddress);
        //            }
        //        }
        //        foreach (var attach in message.Attachments)
        //        {
        //            Attachment attachment;
        //            if (attach.Content != null)
        //            {
        //                attachment = AddAttachment(attach.Content, attach.FileName);
        //            }
        //            else
        //            {
        //                attachment = AddAttachment(attach.FileName);
        //            }
        //            email.Attachments.Add(attachment);
        //        }
        //        email.IsBodyHtml = true;
        //        email.Subject = message.Subject;
        //        email.Body = message.Body;
        //        email.Priority = message.MailPriority;
        //        //    email.From = new MailAddress(message.From,message.FriendlyName);

        //        #region Set Embedded Content
        //        if (message.EmbeddedContent != null)
        //        {
        //            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(message.Body, Encoding.UTF8, MediaTypeNames.Text.Html);
        //            AlternateView plainView = AlternateView.CreateAlternateViewFromString(Regex.Replace(message.Body, "<[^>]+?>", string.Empty), Encoding.UTF8, MediaTypeNames.Text.Plain);
        //            string mediaType = MediaTypeNames.Image.Jpeg;
        //            foreach (var ec in message.EmbeddedContent)
        //            {
        //                LinkedResource img = new LinkedResource(ec.Path, mediaType);
        //                img.ContentId = ec.ContentID;
        //                img.ContentType.MediaType = mediaType;
        //                img.TransferEncoding = TransferEncoding.Base64;
        //                img.ContentType.Name = img.ContentId;
        //                img.ContentLink = new Uri("cid:" + img.ContentId);
        //                htmlView.LinkedResources.Add(img);
        //            }
        //            email.AlternateViews.Add(plainView);
        //            email.AlternateViews.Add(htmlView);
        //        }
        //        #endregion
        //        SmtpClient client = new SmtpClient(message.SmtpClientHost);
        //        NetworkCredential basicCredential = new NetworkCredential(message.UserName, message.Password);
        //        client.UseDefaultCredentials = false;
        //        client.Credentials = basicCredential;
        //        client.Port = message.SmtpPort;
        //        client.EnableSsl = message.enableSSL;
        //        client.Host = message.SmtpClientHost;
        //        var client1 = new WebClient();
        //        bool internetCon;

        //        try
        //        {
        //            client.Send(email);              
        //            return true;
        //        }
        //        catch (Exception ex)
        //        {

        //            using (client1.OpenRead("http://google.com"))
        //                internetCon = true;
        //            throw;
        //            //log.Error("Email Error-" + ex.InnerException + ", StackTrace-" + ex.StackTrace + ", DateTimeStamp-" + DateTime.Now);
        //            //log.Error("Internet Connection-" + internetCon + ", StackTrace-" + ex.StackTrace + ", DateTimeStamp-" + DateTime.Now);

        //            throw;
        //        }
        //    }


        //}
        private static Attachment AddAttachment(string file)
        {
            Attachment attachment = new Attachment(file, MediaTypeNames.Application.Octet);
            ContentDisposition disposition = attachment.ContentDisposition;
            disposition.CreationDate = System.IO.File.GetCreationTime(file);
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
            disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
            return attachment;
        }
        private static Attachment AddAttachment(Stream ms, string sFileName)
        {
            Attachment attachment = new Attachment(ms, sFileName);
            return attachment;
        }

        public void Dispose()
        {
        }

    }
    /// <summary>
    /// Email Message Class
    /// </summary>
    public class EmailMessage
    {
        public string UserName { set; get; }
        public string Password { set; get; }
        public string SmtpClientHost { set; get; }
        public int SmtpPort { set; get; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string FriendlyName { get; set; }
        public string CC { get; set; }
        public string Bcc { set; get; }
        public bool enableSSL { set; get; }
        public bool HTMLView { set; get; }
        public List<MailAttachment> Attachments { set; get; }
        public System.Net.Mail.MailPriority MailPriority { set; get; }
        public EmailMessage()
        {
            Attachments = new List<MailAttachment>();
        }
        public List<EmbeddedContent> EmbeddedContent { set; get; }
   
        public List<KeyValuePair<string, string>> PlaceHolders { get; set; }

        public string TemplateName { get; set; }

        public int? CustomerId { get; set; }
        public int SubjectId { get; set; }

    }
    /// <summary>
    /// Mail Attachment Class
    /// </summary>
    public class MailAttachment
    {
        public string FileName { get; set; }
        public Stream Content { get; set; }
    }

    public class EmbeddedContent
    {
        public string ContentID { get; set; }
        public string Path { get; set; }
    }





}
