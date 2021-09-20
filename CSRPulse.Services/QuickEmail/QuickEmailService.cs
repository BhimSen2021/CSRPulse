using CSRPulse.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using CSRPulse.Model;
using DTOModel = CSRPulse.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public class QuickEmailService : BaseService, IQuickEmailService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IEmailService _emailService;

        public QuickEmailService(IGenericRepository genericRepository, IEmailService emailService)
        {
            _genericRepository = genericRepository;
            _emailService = emailService;
        }

        public async Task<bool> SendEmailAsync(QuickEmail quickEmail)
        {
            bool flag = false;
            try
            {
                //StringBuilder emailBody = new StringBuilder("");
                //Common.EmailMessage message = new Common.EmailMessage();
                string to = string.Empty;
                string bcc = string.Empty;

                if (quickEmail.ToEmails != null)
                {
                    for (int i = 0; i < quickEmail.ToEmails.Length; i++)
                        to += Convert.ToString(quickEmail.ToEmails[i]) + ";";
                }
                if (quickEmail.BccEmails != null)
                {
                    for (int i = 0; i < quickEmail.BccEmails.Length; i++)
                        bcc += Convert.ToString(quickEmail.BccEmails[i]) + ";";
                }
                //message.To = to.TrimEnd(';');
                //message.Bcc = bcc.TrimEnd(';');

                //var mailSubj = _genericRepository.Get<DTOModel.MailSubject>(x => x.MailProcessId == (int)Common.MailSubject.QuickEmail).FirstOrDefault();

                //message.Subject = quickEmail.Subject;
                //if (mailSubj != null)
                //{

                //    message.SubjectId = mailSubj.SubjectId;
                //}
                //else
                //    message.Subject = "CSRPulse- QuickEmail Notification";

                //message.PlaceHolders = new List<KeyValuePair<string, string>>();
                //message.TemplateName = "QuickEmail";
                //message.PlaceHolders.Add(new KeyValuePair<string, string>("{$message}", quickEmail.Message));

                var mailDetail = new MailDetail() { Subject = quickEmail.Subject, Message = quickEmail.Message, To = to.TrimEnd(';'), Bcc = bcc.TrimEnd(';') };
                flag = await _emailService.SendMail(mailDetail, Common.MailProcess.Maintenance);

                // flag = await _emailService.PrepareTemplate(message);

            }
            catch (Exception)
            {
                flag = false;
            }

            return flag;
        }
    }
}
