using CSRPulse.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public interface IFinancialAuditReportService
    {
        Task<FinancialAuditReport> CreateFinancialAsync(FinancialAuditReport financial);
        Task<FinancialAuditReport> GetFinancialByIdAsync(int fareportId);
        Task<List<FinancialAuditReport>> GetFinancialAsync();
        Task<List<FinancialAuditReport>> GetFinancialAsync(FinancialAuditReport financial);
        Task<bool> UpdateFinancialAsync(FinancialAuditReport financial);
        bool ActiveDeActive(int id, bool IsActive);
        
    }
}
