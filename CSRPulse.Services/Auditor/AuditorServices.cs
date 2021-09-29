using AutoMapper;
using CSRPulse.Data.Repositories;
using CSRPulse.Model;
using DTOModel = CSRPulse.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CSRPulse.Common;

namespace CSRPulse.Services
{
    public class AuditorServices : BaseService, IAuditorServices
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;

        public AuditorServices(IGenericRepository generic, IMapper mapper)
        {
            _genericRepository = generic;
            _mapper = mapper;
        }

        public async Task<Auditor> CreateAuditorAsync(Auditor auditor)
        {
            try
            {
                var model = _mapper.Map<DTOModel.Auditor>(auditor);
                var IsExist = _genericRepository.Exists<DTOModel.Auditor>(x => x.AuditOrganization.ToLower() == auditor.AuditOrganization.ToLower());
                if (!IsExist)
                {
                    auditor.AuditorId = await _genericRepository.InsertAsync(model);
                }
                auditor.IsExist = IsExist;
                return auditor;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Auditor GetAuditorById(int auditorId)
        {
            try
            {
                var auditors = _genericRepository.GetIQueryable<DTOModel.Auditor>(a => a.AuditorId == auditorId).Include(a => a.AuditorDocument).ThenInclude(d => d.Document);

                var resList = _mapper.Map<IEnumerable<DTOModel.Auditor>, IEnumerable<Auditor>>(auditors);
                var res = resList.FirstOrDefault();

                if (res != null)
                    return res;
                else
                    return new Auditor();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<Auditor>> GetAuditorAsync(Auditor auditor)
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.Auditor>(x => (auditor.IsActiveFilter.HasValue ? x.IsActive == auditor.IsActiveFilter.Value : 1 > 0));
                return _mapper.Map<List<Auditor>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateAuditorAsync(Auditor auditor)
        {
            try
            {
                var IsExist = _genericRepository.Exists<DTOModel.Auditor>(x => x.AuditOrganization.ToLower() == auditor.AuditOrganization.ToLower() && x.AuditorId != auditor.AuditorId);

                var auditorDocuments = _mapper.Map<List<DTOModel.AuditorDocument>>(auditor.AuditorDocument);

                auditor.IsExist = IsExist;

                if (!IsExist)
                {
                    var result = await _genericRepository.GetByIDAsync<DTOModel.Auditor>(auditor.AuditorId);
                    if (result != null)
                    {
                        result.AuditOrganization = auditor.AuditOrganization;
                        result.Phone = auditor.Phone;
                        result.Email = auditor.Email;
                        result.Website = auditor.Website;
                        result.Address = auditor.Address;
                        result.City = auditor.City;
                        result.StateId = auditor.StateId;
                        result.PinCode = auditor.PinCode;
                        result.Gstno = auditor.Gstno;
                        result.Pan = auditor.Pan;
                        result.IsActive = auditor.IsActive;
                        result.UpdatedOn = auditor.UpdatedOn;
                        result.UpdatedBy = auditor.UpdatedBy;
                        result.UpdatedRid = auditor.UpdatedRid;
                        result.UpdatedRname = auditor.UpdatedRname;
                        _genericRepository.Update(result);

                    }
                    var oldDocument = await _genericRepository.GetAsync<DTOModel.AuditorDocument>(x => x.AuditorId == auditor.AuditorId);
                    if (oldDocument != null && oldDocument.ToList().Count > 0)
                    {
                        await _genericRepository.RemoveMultipleEntityAsync<DTOModel.AuditorDocument>(oldDocument);
                        await _genericRepository.AddMultipleEntityAsync(auditorDocuments);
                    }
                    else
                    {
                        _genericRepository.AddMultipleEntity(auditorDocuments);
                    }
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ActiveDeActive(int id, bool IsActive)
        {
            try
            {
                var model = _genericRepository.GetByID<DTOModel.Auditor>(id);
                model.IsActive = IsActive;
                _genericRepository.Update(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Model.Document>> GetAuditorDocument(int processId)
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.ProcessDocument>(x => x.ProcessId == processId && x.IsActive == true);
                if (result != null)
                {
                    return result.Select(d => new Model.Document()
                    {
                        DocumentId = d.DocumentId,
                        DocumentName = d.DocumentName,
                        DocumentMaxSize = d.DocumentMaxSize ?? 20,
                        DocumentType = ExtensionMethods.GetUploadDocumentType(d.DocumentType),
                        Mandatory = d.Mandatory ?? false,
                        Remark = d.Remark
                    }).ToList();
                }
                else
                    return new List<Model.Document>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<AuditorDocument>> GetAuditorDocumentList(int auditorId, int processId)
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.AuditorDocument>(x => x.AuditorId == auditorId);
                if (result != null)
                {
                    return result.Select(d => new AuditorDocument()
                    {
                        AuditorDocumentId = d.AuditorDocumentId,
                        AuditorId = d.AuditorId,
                        DocumentId = d.DocumentId,
                        DocumentName = d.DocumentName,
                        UploadFileName = d.UploadFileName,
                        ServerFileName = d.ServerFileName,
                        DocumentMaxSize = d.DocumentMaxSize ?? 20,
                        DocumentType = d.DocumentType,
                        Mandatory = d.Mandatory ?? false,
                        Remark = d.Remark,
                        CreatedBy = d.CreatedBy,
                        CreatedOn = d.CreatedOn,
                        CreatedRid = d.CreatedRid,
                        CreatedRname = d.CreatedRname
                    }).ToList();
                }

                else
                    return new List<AuditorDocument>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> AddDocument(AuditorDocument auditorDocument)
        {
            try
            {
                int flag = 1;
                var model = _mapper.Map<DTOModel.AuditorDocument>(auditorDocument);
                if (!await _genericRepository.ExistsAsync<DTOModel.AuditorDocument>
                      (x => x.DocumentId == auditorDocument.DocumentId && x.AuditorId == auditorDocument.AuditorId))

                    await _genericRepository.InsertAsync<DTOModel.AuditorDocument>(model);
                else
                    flag = 2;

                return flag;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
