using CSRPulse.Data.Repositories;
using CSRPulse.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DTOModel = CSRPulse.Data.Models;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Options;
using static CSRPulse.Common.DocumentUploadFilePath;
using System.Net.Mail;
using CSRPulse.Common;

namespace CSRPulse.Services
{
    public class EmailService : BaseService, IEmailService
    {
        // private const string templatePath = @"wwwroot/Templates/EmailTemplate/{0}.html";
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        private readonly SMTPConfig _mailSetting;

        public EmailService(IGenericRepository genericRepository, IMapper mapper, IOptions<SMTPConfig> mailSetting)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _mailSetting = mailSetting.Value;

        }

        #region  Pre define method for email process, do not add anything in these method.
        private string GetEmailBody(string templateName)
        {
            var body = File.ReadAllText(string.Format(EmailTemplatePath, templateName));
            return body;
        }

        private string UpdatePlaceHolders(string text, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if (!string.IsNullOrEmpty(text) && keyValuePairs != null)
            {
                foreach (var placeholder in keyValuePairs)
                {
                    if (text.Contains(placeholder.Key))
                    {
                        text = text.Replace(placeholder.Key, placeholder.Value);
                    }
                }
            }

            return text;
        }

        async Task<bool> SendEmail(Common.EmailMessage message)
        {
            DTOModel.MailSendStatus dtoMailStatus = new DTOModel.MailSendStatus();
            try
            {
                dtoMailStatus = _mapper.Map<DTOModel.MailSendStatus>(message);
                dtoMailStatus.SentOn = DateTime.Now;
                dtoMailStatus.Status = false;

                var emailsetting = _genericRepository.Get<DTOModel.EmailConfiguration>().FirstOrDefault();

                message.From = emailsetting.UserName;
                message.UserName = emailsetting.UserName;
                message.Password = emailsetting.Password;
                message.SmtpClientHost = emailsetting.Server;
                message.SmtpPort = emailsetting.Port;
                message.enableSSL = emailsetting.Sslstatus;
                message.HTMLView = true;
                message.FriendlyName = emailsetting.FriendlyName;
                var res = await EmailHelper.SendEmail(message);

                dtoMailStatus.Status = res;
                await SendMailStatusAsync(dtoMailStatus, _genericRepository);
                return res;
            }
            catch (Exception)
            {
                await SendMailStatusAsync(dtoMailStatus, _genericRepository);
                return false;
            }
        }

        #endregion

        public async Task<bool> SendMail(MailDetail mailDetail, MailProcess mailProcess)
        {
            bool flag = false;
            try
            {
                EmailMessage message = new EmailMessage();
                message.To = mailDetail.To;
                message.CC = mailDetail.CC;
                message.Bcc = mailDetail.Bcc;


                #region Get Email Subject
                if (string.IsNullOrEmpty(mailDetail.Subject))
                {
                    var mailSubj = _genericRepository.Get<DTOModel.MailSubject>(x => x.MailProcessId == (int)mailProcess).FirstOrDefault();
                    if (mailSubj != null)
                    {
                        message.HeaderImage = mailSubj.HeaderImage;
                        message.Subject = mailSubj.Subject;
                        message.SubjectId = mailSubj.SubjectId;
                        message.Signature = mailSubj.Signature;
                        message.Disclaimer = mailSubj.Disclaimer;
                        message.TermsConditions = mailSubj.TermsConditions;
                        message.ContactUs = mailSubj.ContactUs;
                        message.DisplayContactUs = ExtensionMethods.MakeDisplayEmail(mailSubj.ContactUs);
                    }
                    else
                        message.Subject = "Axis Bank CSR";
                }
                else
                    message.Subject = mailDetail.Subject;
                #endregion


                message.TemplateName = mailProcess.ToString();

                message.PlaceHolders = new List<KeyValuePair<string, string>>();
                switch (mailProcess)
                {
                    case MailProcess.Registration:                      
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$fullName}", mailDetail.FullName));
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$userName}", mailDetail.UserName));
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$password}", mailDetail.Password));
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$headerImage}", message.HeaderImage));
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$signature}", message.Signature));
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$contactUs}", message.ContactUs));
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$displayContactUs}", message.DisplayContactUs));
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$disclaimer}", message.Disclaimer));
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$termsConditions}", message.TermsConditions));
                        break;

                    case MailProcess.OneTimePassword:
                        
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$fullName}", mailDetail.FullName));
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$otp}", mailDetail.OTP));
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$headerImage}", message.HeaderImage));
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$signature}", message.Signature));
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$contactUs}", message.ContactUs));
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$displayContactUs}", message.DisplayContactUs));
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$disclaimer}", message.Disclaimer));
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$termsConditions}", message.TermsConditions));
                        break;

                    case MailProcess.ResetPassword:
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$fullName}", mailDetail.FullName));
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$password}", mailDetail.Password));
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$headerImage}", message.HeaderImage));
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$signature}", message.Signature));
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$contactUs}", message.ContactUs));
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$displayContactUs}", message.DisplayContactUs));
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$disclaimer}", message.Disclaimer));
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$termsConditions}", message.TermsConditions));
                        break;

                    case MailProcess.Maintenance:
                    case MailProcess.QuickEmail:
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$message}", mailDetail.Message));
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$message2}", mailDetail.Message2));
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$headerImage}", message.HeaderImage));
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$signature}", message.Signature));
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$contactUs}", message.ContactUs));
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$displayContactUs}", message.DisplayContactUs));
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$disclaimer}", message.Disclaimer));
                        message.PlaceHolders.Add(new KeyValuePair<string, string>("{$termsConditions}", message.TermsConditions));
                        break;

                    default:
                        break;
                }
                flag = await PrepareTemplate(message);
            }
            catch (Exception)
            {
                throw;
            }
            return flag;

        }
        async Task<bool> PrepareTemplate(EmailMessage message)
        {
            try
            {
                message.Subject = UpdatePlaceHolders(message.Subject, message.PlaceHolders);
                message.Body = UpdatePlaceHolders(GetEmailBody(message.TemplateName), message.PlaceHolders);
                return await SendEmail(message);
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
