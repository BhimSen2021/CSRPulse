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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CSRPulse.Services
{
    public class EmailService : BaseService, IEmailService
    {
        private const string templatePath = @"wwwroot/Templates/EmailTemplate/{0}.html";
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
            var body = File.ReadAllText(string.Format(templatePath, templateName));
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

                message.From = $"{_mailSetting.DisplayName} <" + _mailSetting.SenderAddress + ">";
                message.UserName = _mailSetting.UserName;
                message.Password = _mailSetting.Password;
                message.SmtpClientHost = _mailSetting.Host;
                message.SmtpPort = _mailSetting.Port;
                message.enableSSL = _mailSetting.EnableSSL;
                message.HTMLView = true;
                message.FriendlyName = emailsetting !=null ? emailsetting.Signature : _mailSetting.Signature;
                var res = await Common.EmailHelper.SendEmail(message);

                dtoMailStatus.Status = res;
                return await SendMailStatusAsync(dtoMailStatus, _genericRepository);
            }
            catch (Exception)
            {
                await SendMailStatusAsync(dtoMailStatus, _genericRepository);
                return false;
            }
        }

        #endregion

        public async Task<bool> CustomerRelatedMails(Common.EmailMessage message)
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
