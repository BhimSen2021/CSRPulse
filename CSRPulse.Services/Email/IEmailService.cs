using CSRPulse.Common;
using CSRPulse.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public interface IEmailService
    {
        Task<bool> SendMail(MailDetail mailDetail, MailProcess mailProcess);


    }
}
