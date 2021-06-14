using CSRPulse.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public interface IMaintenanceService
    {
        Task<bool> GoUnderMaintenanceAsync(Maintenance maintenance);
        Maintenance GetMaintenanceDetails();
        void UpdateMaintenance(bool IsMaintenance);
        bool IsUnderMaintenance();
        Task<bool> SendEmailAsync(string Message);
    }
}
