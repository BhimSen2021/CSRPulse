using AutoMapper;
using CSRPulse.Data.Repositories;
using CSRPulse.Model;
using DTOModel = CSRPulse.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using CSRPulse.Common;


namespace CSRPulse.Services
{
    public class PartnerService : BaseService, IPartnerService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IPartnerRepository _partnerRepository;
        private readonly IMapper _mapper;
        public PartnerService(IGenericRepository generic, IPartnerRepository partnerRepository, IMapper mapper)
        {
            _genericRepository = generic;
            _partnerRepository = partnerRepository;
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
                var partner = await _genericRepository.GetByIDAsync<DTOModel.Partner>(PartnerId);
                if (partner != null)
                {
                    if (partner.PartnerType == (int)Common.PartnerType.NGO)
                    {
                        var NGOpartner = _genericRepository.GetIQueryable<DTOModel.Partner>(p => p.PartnerId == PartnerId).Include(a => a.NgoawardDetail)
                            .Include(b => b.NgosaturatoryAuditorDetail)
                            .Include(k => k.NgokeyProjects)
                            .Include(f => f.NgocorpusGrantFund)
                            .Include(r => r.NgoregistrationDetail)
                            .Include(m => m.Ngomember)
                            .Include(c => c.NgochartDocument)
                            .Include(f => f.NgofundingPartner).ThenInclude(a => a.FundingAgency);

                        var res = _mapper.Map<IEnumerable<DTOModel.Partner>, IEnumerable<Partner>>(NGOpartner);

                        return res.FirstOrDefault();
                    }
                    return _mapper.Map<Partner>(partner);
                }
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
                    dData.RegPhone = partner.RegPhone;
                    dData.ComPin = partner.ComPin;
                    dData.ComState = partner.ComState;
                    dData.CommPhone = partner.CommPhone;
                    dData.CommMobile = partner.CommMobile;
                    dData.IsActive = partner.IsActive;
                    //dData.UpdatedBy = partner.UpdatedBy;
                    //dData.UpdatedOn = partner.UpdatedOn;
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

        public async Task<List<NgoawardDetail>> GetUpdateNGOAwardDetails(Partner partner)
        {
            try
            {
                var model = _mapper.Map<List<DTOModel.NgoawardDetail>>(partner.NgoawardDetail);
                var isExist = await _genericRepository.ExistsAsync<DTOModel.NgoawardDetail>(x => x.PartnerId == partner.PartnerId);
                if (!isExist)
                {
                    await _genericRepository.AddMultipleEntityAsync(model);
                }
                else
                {
                    var oldDetails = await _genericRepository.GetAsync<DTOModel.NgoawardDetail>(x => x.PartnerId == partner.PartnerId);
                    if (oldDetails != null && oldDetails.ToList().Count > 0)
                    {
                        await _genericRepository.RemoveMultipleEntityAsync<DTOModel.NgoawardDetail>(oldDetails);
                        await _genericRepository.AddMultipleEntityAsync(model);
                    }
                }
                var result = await _genericRepository.GetAsync<DTOModel.NgoawardDetail>(x => x.PartnerId == partner.PartnerId);

                return _mapper.Map<List<NgoawardDetail>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<NgosaturatoryAuditorDetail>> GetUpdateSaturatoryAuditorDetails(Partner partner)
        {
            try
            {
                var model = _mapper.Map<List<DTOModel.NgosaturatoryAuditorDetail>>(partner.NgosaturatoryAuditorDetail);
                var isExist = await _genericRepository.ExistsAsync<DTOModel.NgosaturatoryAuditorDetail>(x => x.PartnerId == partner.PartnerId);
                if (!isExist)
                {
                    await _genericRepository.AddMultipleEntityAsync(model);
                }
                else
                {
                    var oldDetails = await _genericRepository.GetAsync<DTOModel.NgosaturatoryAuditorDetail>(x => x.PartnerId == partner.PartnerId);
                    if (oldDetails != null && oldDetails.ToList().Count > 0)
                    {
                        await _genericRepository.RemoveMultipleEntityAsync<DTOModel.NgosaturatoryAuditorDetail>(oldDetails);
                        await _genericRepository.AddMultipleEntityAsync(model);
                    }
                }
                var result = await _genericRepository.GetAsync<DTOModel.NgosaturatoryAuditorDetail>(x => x.PartnerId == partner.PartnerId);

                return _mapper.Map<List<NgosaturatoryAuditorDetail>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<NGOKeyProjects>> GetUpdateNGOKeyProjects(Partner partner)
        {
            try
            {
                var model = _mapper.Map<List<DTOModel.NgokeyProjects>>(partner.NGOKeyProjects);
                var isExist = await _genericRepository.ExistsAsync<DTOModel.NgokeyProjects>(x => x.PartnerId == partner.PartnerId);
                if (!isExist)
                {
                    await _genericRepository.AddMultipleEntityAsync(model);
                }
                else
                {
                    var oldDetails = await _genericRepository.GetAsync<DTOModel.NgokeyProjects>(x => x.PartnerId == partner.PartnerId);
                    if (oldDetails != null && oldDetails.ToList().Count > 0)
                    {
                        await _genericRepository.RemoveMultipleEntityAsync<DTOModel.NgokeyProjects>(oldDetails);
                        await _genericRepository.AddMultipleEntityAsync(model);
                    }
                }
                var result = await _genericRepository.GetAsync<DTOModel.NgokeyProjects>(x => x.PartnerId == partner.PartnerId);

                return _mapper.Map<List<NGOKeyProjects>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<NGOCorpusGrantFund>> GetUpdateNGOCorpusGrantFund(Partner partner, int fundType)
        {
            try
            {

                var model = _mapper.Map<List<DTOModel.NgocorpusGrantFund>>(partner.NgocorpusGrantFund);
                var isExist = await _genericRepository.ExistsAsync<DTOModel.NgocorpusGrantFund>(x => x.PartnerId == partner.PartnerId && x.FundType == fundType);
                if (!isExist)
                {
                    await _genericRepository.AddMultipleEntityAsync(model);
                }
                else
                {
                    var oldDetails = await _genericRepository.GetAsync<DTOModel.NgocorpusGrantFund>(x => x.PartnerId == partner.PartnerId && x.FundType == fundType);
                    if (oldDetails != null && oldDetails.ToList().Count > 0)
                    {
                        await _genericRepository.RemoveMultipleEntityAsync<DTOModel.NgocorpusGrantFund>(oldDetails);
                        await _genericRepository.AddMultipleEntityAsync(model);
                    }
                }
                var result = await _genericRepository.GetAsync<DTOModel.NgocorpusGrantFund>(x => x.PartnerId == partner.PartnerId && x.FundType == fundType);

                return _mapper.Map<List<NGOCorpusGrantFund>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<NGORegistrationDetail>> GetUpdateNGORegistration(Partner partner)
        {
            try
            {
                var model = _mapper.Map<List<DTOModel.NgoregistrationDetail>>(partner.NgoregistrationDetail);
                var isExist = await _genericRepository.ExistsAsync<DTOModel.NgoregistrationDetail>(x => x.PartnerId == partner.PartnerId);
                if (!isExist)
                {
                    await _genericRepository.AddMultipleEntityAsync(model);
                }
                else
                {
                    var oldDetails = await _genericRepository.GetAsync<DTOModel.NgoregistrationDetail>(x => x.PartnerId == partner.PartnerId);
                    if (oldDetails != null && oldDetails.ToList().Count > 0)
                    {
                        await _genericRepository.RemoveMultipleEntityAsync<DTOModel.NgoregistrationDetail>(oldDetails);
                        await _genericRepository.AddMultipleEntityAsync(model);
                    }
                }
                var result = await _genericRepository.GetAsync<DTOModel.NgoregistrationDetail>(x => x.PartnerId == partner.PartnerId);

                return _mapper.Map<List<NGORegistrationDetail>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<NGOChartDocument>> GetUpdateNGOChartDocument(Partner partner)
        {
            try
            {
                var model = _mapper.Map<List<DTOModel.NgochartDocument>>(partner.NgochartDocument);
                var isExist = await _genericRepository.ExistsAsync<DTOModel.NgochartDocument>(x => x.PartnerId == partner.PartnerId);
                if (!isExist)
                {
                    await _genericRepository.AddMultipleEntityAsync(model);
                }
                else
                {
                    var oldDetails = await _genericRepository.GetAsync<DTOModel.NgochartDocument>(x => x.PartnerId == partner.PartnerId);
                    if (oldDetails != null && oldDetails.ToList().Count > 0)
                    {
                        await _genericRepository.RemoveMultipleEntityAsync<DTOModel.NgochartDocument>(oldDetails);
                        await _genericRepository.AddMultipleEntityAsync(model);
                    }
                }
                var result = await _genericRepository.GetAsync<DTOModel.NgochartDocument>(x => x.PartnerId == partner.PartnerId);

                return _mapper.Map<List<NGOChartDocument>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<NGOMember>> GetInsertNGOMember(NGOMember member)
        {
            try
            {
                var model = _mapper.Map<DTOModel.Ngomember>(member);

                await _genericRepository.InsertAsync(model);

                var result = await _genericRepository.GetAsync<DTOModel.Ngomember>(x => x.PartnerId == member.PartnerId && x.MemberType == member.MemberType);

                return _mapper.Map<List<NGOMember>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<NGOMember>> GetDeleteNGOMember(int id, int partnerId, int memberType)
        {
            try
            {
                _genericRepository.Delete<DTOModel.Ngomember>(id);

                var result = await _genericRepository.GetAsync<DTOModel.Ngomember>(x => x.PartnerId == partnerId && x.MemberType == memberType);

                return _mapper.Map<List<NGOMember>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<NGOFundingPartner>> GetInsertNGOFundingPartner(NGOFundingPartner fundingPartner)
        {
            try
            {
                var model = _mapper.Map<DTOModel.NgofundingPartner>(fundingPartner);
                await _genericRepository.InsertAsync(model);

                var result = _genericRepository.GetIQueryable<DTOModel.NgofundingPartner>(x => x.PartnerId == fundingPartner.PartnerId && x.AgencyType == fundingPartner.AgencyType).Include(a => a.FundingAgency);

                return _mapper.Map<List<NGOFundingPartner>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<NGOFundingPartner> GetDeleteNGOFundingPartner(int id, int partnerId, int agencyType)
        {
            try
            {
                _genericRepository.Delete<DTOModel.NgofundingPartner>(id);

                var result = _genericRepository.GetIQueryable<DTOModel.NgofundingPartner>(x => x.PartnerId == partnerId && x.AgencyType == agencyType).Include(a => a.FundingAgency);

                return _mapper.Map<List<NGOFundingPartner>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<NGOFundingPartner> GetNGOFundingPartner(int id)
        {
            try
            {
                var result = await _genericRepository.GetByIDAsync<NGOFundingPartner>(id);
                return _mapper.Map<NGOFundingPartner>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<PartnerDocument>> GetPartnerDocumentAsync(int partnerId)
        {
            try
            {
                var result = await _partnerRepository.GetDocuments(partnerId).OrderBy(s => s.DocumentId).ToListAsync();
                if (result != null)
                {
                    return result.Select(a => new PartnerDocument()
                    {
                        PartnerId = a.PartnerId,
                        DocumentId = a.DocumentId,
                        DocumentName = a.DocumentName,
                        UploadedDocumentName=a.UploadedDocumentName,
                        ServerDocumentName=a.ServerDocumentName,
                        PartnerDocumentId=a.PartnerDocumentId,
                        Remark=a.Remark,
                        IsUploaded = a.IsUploaded
                    }).ToList();
                }
                else
                    return new List<PartnerDocument>();
            }
            catch (Exception)
            {
                throw;
            }
        }

       public async Task<List<PartnerPolicyModule>> GetPartnerPolicyModuleAsync()
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.PartnerPolicyModule>(x=>x.IsActive==true);
                return _mapper.Map<List<PartnerPolicyModule>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<PartnerPolicy>> GetPartnerPolicyAsync()
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.PartnerPolicy>(x => x.IsActive == true);
                return _mapper.Map<List<PartnerPolicy>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateDocument(NGOChartDocument partnerlist)
        {
            try
            {
                var model = _mapper.Map<DTOModel.NgochartDocument>(partnerlist);
                await _genericRepository.UpdateAsync(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteDocument(int ngocharId)
        {
            try
            {
                _genericRepository.Delete<DTOModel.NgochartDocument>(ngocharId);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<NGOChartDocument>> GetDocumentList(int PartnerId)
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.NgochartDocument>(x => x.PartnerId == PartnerId);
                return _mapper.Map<List<NGOChartDocument>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<PartnerDocument>> GetPartnerDocumentList(int partnerId, int processId)
        {
            try
            {
                var result = await _partnerRepository.GetPartnerDocuments(partnerId,processId).ToListAsync();
                if (result != null)
                {
                    return result.Select(d => new PartnerDocument()
                    {
                        PartnerDocumentId = d.PartnerDocumentId,
                        PartnerId = d.PartnerId,
                        DocumentId = d.DocumentId,
                        DocumentName = d.DocumentName,
                        UploadedDocumentName = d.UploadedDocumentName,
                        ServerDocumentName = d.ServerDocumentName,
                        Remark = d.Remark,
                        IsUploaded = d.IsUploaded,
                        DocumentMaxSize = d.DocumentMaxSize,
                        DocumentType = ExtensionMethods.GetUploadDocumentType(d.DocumentType),
                        Mandatory = d.Mandatory,
                    }).ToList();
                }
                else
                    return new List<PartnerDocument>();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<PartnerDocument>> GetUpdatePartnerDocument(Partner partner)
        {
            try
            {
                var model = _mapper.Map<List<DTOModel.PartnerDocument>>(partner.PartnerDocument);
                var isExist = await _genericRepository.ExistsAsync<DTOModel.PartnerDocument>(x => x.PartnerId == partner.PartnerId);
                if (!isExist)
                {
                    await _genericRepository.AddMultipleEntityAsync(model);
                }
                else
                {
                    var oldDetails = await _genericRepository.GetAsync<DTOModel.PartnerDocument>(x => x.PartnerId == partner.PartnerId);
                    if (oldDetails != null && oldDetails.ToList().Count > 0)
                    {
                        await _genericRepository.RemoveMultipleEntityAsync<DTOModel.PartnerDocument>(oldDetails);
                        await _genericRepository.AddMultipleEntityAsync(model);
                    }
                }
                var result = await _genericRepository.GetAsync<DTOModel.PartnerDocument>(x => x.PartnerId == partner.PartnerId);

                return _mapper.Map<List<PartnerDocument>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeletePartnerDocument(int partnerId)
        {
            try
            {
                _genericRepository.Delete<DTOModel.PartnerDocument>(partnerId);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<PartnerPolicyDetails>> GetUpdatePartnerPolicyDetails(Partner partner)
        {
            try
            {
                var model = _mapper.Map<List<DTOModel.PartnerPolicyDetails>>(partner.PartnerPolicyDetails);
                var isExist = await _genericRepository.ExistsAsync<DTOModel.PartnerPolicyDetails>(x => x.PartnerId == partner.PartnerId);
                if (!isExist)
                {
                    await _genericRepository.AddMultipleEntityAsync(model);
                }
                else
                {
                    var oldDetails = await _genericRepository.GetAsync<DTOModel.PartnerPolicyDetails>(x => x.PartnerId == partner.PartnerId);
                    if (oldDetails != null && oldDetails.ToList().Count > 0)
                    {
                        await _genericRepository.RemoveMultipleEntityAsync<DTOModel.PartnerPolicyDetails>(oldDetails);
                        await _genericRepository.AddMultipleEntityAsync(model);
                    }
                }
                var result = await _genericRepository.GetAsync<DTOModel.PartnerPolicyDetails>(x => x.PartnerId == partner.PartnerId);

                return _mapper.Map<List<PartnerPolicyDetails>>(result);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<List<PartnerPolicyDetails>> GetPartnerPolicyDetailsList(int PartnerId)
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.PartnerPolicyDetails>(x => x.PartnerId == PartnerId);
                return _mapper.Map<List<PartnerPolicyDetails>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<PartnerPolicyModuleDetails>> GetUpdatePartnerPolicyModuleDetails(Partner partner)
        {
            try
            {
                var model = _mapper.Map<List<DTOModel.PartnerPolicyModuleDetails>>(partner.PartnerPolicyModuleDetails);
                var isExist = await _genericRepository.ExistsAsync<DTOModel.PartnerPolicyModuleDetails>(x => x.PartnerId == partner.PartnerId);
                if (!isExist)
                {
                    await _genericRepository.AddMultipleEntityAsync(model);
                }
                else
                {
                    var oldDetails = await _genericRepository.GetAsync<DTOModel.PartnerPolicyModuleDetails>(x => x.PartnerId == partner.PartnerId);
                    if (oldDetails != null && oldDetails.ToList().Count > 0)
                    {
                        await _genericRepository.RemoveMultipleEntityAsync<DTOModel.PartnerPolicyModuleDetails>(oldDetails);
                        await _genericRepository.AddMultipleEntityAsync(model);
                    }
                }
                var result = await _genericRepository.GetAsync<DTOModel.PartnerPolicyModuleDetails>(x => x.PartnerId == partner.PartnerId);

                return _mapper.Map<List<PartnerPolicyModuleDetails>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<PartnerPolicyModuleDetails>> GetPartnerPolicyModuleDetailsList(int PartnerId)
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.PartnerPolicyModuleDetails>(x => x.PartnerId == PartnerId);
                return _mapper.Map<List<PartnerPolicyModuleDetails>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
