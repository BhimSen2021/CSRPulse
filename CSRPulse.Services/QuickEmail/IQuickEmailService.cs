using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSRPulse.Model;
namespace CSRPulse.Services
{
   public interface IQuickEmailService
    {
        Task<bool> SendEmailAsync(QuickEmail quickEmail);
    }
}
