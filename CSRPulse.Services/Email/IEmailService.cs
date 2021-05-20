using CSRPulse.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public interface IEmailService
    {
        Task<bool> CustomerRelatedMails(Common.EmailMessage message);
    
    }
}
