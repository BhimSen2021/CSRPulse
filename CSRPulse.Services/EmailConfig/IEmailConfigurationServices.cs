using CSRPulse.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public interface IEmailConfigurationServices
    {
        Task<bool> UpdateEmailConfig(EmailConfiguration emailConfiguration);
        //Task<EmailConfiguration> GetEmailConfigurationById(int EmailConfigurationID);
        Task<EmailConfiguration> GetEmailConfigAsync();
    }
}
