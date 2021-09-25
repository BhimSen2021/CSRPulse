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

                var mailDetail = new MailDetail() { Subject = quickEmail.Subject, Message = quickEmail.Message, To = to.TrimEnd(';'), Bcc = bcc.TrimEnd(';') };
                flag = await _emailService.SendMail(mailDetail, Common.MailProcess.Maintenance);
               
            }
            catch (Exception)
            {
                flag = false;
            }

            return flag;
        }
    }
}
