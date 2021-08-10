using CSRPulse.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSRPulse.IServices
{
    public interface IProcessDocumentService
    {
        Task<List<Model.ProcessDocument>> GetProcessDocuments(int processId);
        Task<bool> CreateProcessDocument(Model.ProcessDocument pDocument);
        Task<Model.ProcessDocument> GetProcessDocumentById(int pDocumentId);
        Task<bool> UpdateProcessDocument(Model.ProcessDocument pDocument);

        IEnumerable<Model.SelectListModel> GetProcess();
        IEnumerable<Model.SelectListModel> GetParentDocument();

    }
}
