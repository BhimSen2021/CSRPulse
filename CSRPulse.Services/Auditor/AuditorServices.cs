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
    }
}
