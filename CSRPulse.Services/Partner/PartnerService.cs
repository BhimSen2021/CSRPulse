using AutoMapper;
using CSRPulse.Data.Repositories;
using CSRPulse.Model;
using DTOModel = CSRPulse.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public class PartnerService : BaseService, IPartnerService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        public PartnerService(IGenericRepository generic, IMapper mapper)
        {
            _genericRepository = generic;
            _mapper = mapper;
        }

        public async Task<bool> CreatePartner(Partner partner)
        {
            try
            {
                var model = _mapper.Map<DTOModel.Partner>(partner);

                var res = await _genericRepository.InsertAsync(model);
                if (res > 0)
                    return true;
                else
                    return false;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Partner> GetPartnerById(int PartnerId)
        {
            try
            {
                var designation = await _genericRepository.GetByIDAsync<DTOModel.Partner>(PartnerId);
                if (designation != null)
                    return _mapper.Map<Partner>(designation);
                else
                    return new Partner();

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> UpdatePartner(Partner partner)
        {
            try
            {
                var dData = await _genericRepository.GetByIDAsync<DTOModel.Partner>(partner.PartnerId);
                if (dData != null)
                {                   

                    dData.PartnerName = partner.PartnerName;
                    dData.PartnerType = partner.PartnerType;
                    dData.Email = partner.Email;
                    dData.Website = partner.Website;
                    dData.DateOfEst = partner.DateOfEst;
                    dData.DealingWithUsSince = partner.DealingWithUsSince;
                    dData.PartnerDetails = partner.PartnerDetails;
                    dData.MissionVision = partner.MissionVision;
                    dData.RegAddress = partner.RegAddress;
                    dData.RegCity = partner.RegCity;
                    dData.RegPin = partner.RegPin;
                    dData.RegState = partner.RegState;
                    dData.RegMobile = partner.RegMobile;
                    dData.CommAddress = partner.CommAddress;
                    dData.ComPin = partner.ComPin;
                    dData.ComState = partner.ComState;
                    dData.CommPhone = partner.CommPhone;
                    dData.CommMobile = partner.CommMobile;
                    dData.IsActive = partner.IsActive;
                    dData.UpdatedBy = partner.UpdatedBy;
                    dData.UpdatedOn = partner.UpdatedOn;
                    _genericRepository.Update(dData);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> RecordExist(Partner partner)
        {
            try
            {
                return await _genericRepository.ExistsAsync<DTOModel.Partner>(x => x.PartnerName == partner.PartnerName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Partner>> GetPartnerAsync(Partner partner)
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.Partner>(x => (partner.IsActiveFilter.HasValue ? x.IsActive == partner.IsActiveFilter.Value : 1 > 0));
                return _mapper.Map<List<Partner>>(result);
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
                var model = _genericRepository.GetByID<DTOModel.Partner>(id);
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
