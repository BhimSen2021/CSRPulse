using AutoMapper;
using CSRPulse.Data.Models;
using CSRPulse.Data.Repositories;
using CSRPulse.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DTOModel = CSRPulse.Data.Models;
namespace CSRPulse.Services
{
    public class ProcessDocumentService : BaseService, IProcessDocumentService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;

        public ProcessDocumentService(IGenericRepository genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }
        public async Task<bool> CreateProcessDocument(Model.ProcessDocument pDocument)
        {
            try
            {
                var dtoProcessDoc = _mapper.Map<DTOModel.ProcessDocument>(pDocument);
                await _genericRepository.InsertAsync(dtoProcessDoc);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Model.ProcessDocument> GetProcessDocumentById(int pDocumentId)
        {
            var result = await _genericRepository.GetFirstOrDefaultAsync<DTOModel.ProcessDocument>(x => x.DocumentId == pDocumentId && x.IsActive == true);
            return _mapper.Map<Model.ProcessDocument>(result);
        }

        public async Task<List<Model.ProcessDocument>> GetProcessDocuments(int processId)
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.ProcessDocument>(x => x.IsActive == true && x.ProcessId == processId);
                return _mapper.Map<List<Model.ProcessDocument>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<bool> UpdateProcessDocument(Model.ProcessDocument pDocument)
        {
            try
            {
                var dtoProcessDoc = _mapper.Map<DTOModel.ProcessDocument>(pDocument);
                _genericRepository.Update(dtoProcessDoc);
                return Task.FromResult(true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Model.SelectListModel> GetProcess()
        {
            try
            {
                var processList = _genericRepository.GetIQueryable<DTOModel.Process>(x => x.Document == true);
                return _mapper.Map<List<Model.SelectListModel>>(processList);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Model.SelectListModel> GetParentDocument()
        {
            try
            {
                var processList = _genericRepository.GetIQueryable<DTOModel.ProcessDocument>(x => x.IsActive == true);
                return _mapper.Map<List<Model.SelectListModel>>(processList);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
