using AutoMapper;
using CSRPulse.Data.Repositories;
using CSRPulse.Model;
using CSRPulse.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOModel = CSRPulse.Data.Models;
namespace CSRPulse.Services
{
    public class DropdownBindService : IDropdownBindService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;

        public DropdownBindService(IGenericRepository generic, IMapper mapper)
        {
            _genericRepository = generic;
            _mapper = mapper;
        }
        public List<SelectListModel> GetCountryAsync(int? countryId)
        {
            var countryList = new List<SelectListModel>()
                {
                    new SelectListModel { id = 1, value = "INDIA" }
                };
            return countryList;
        }
        public List<SelectListModel> GetFundingSourceAsync(int? SourceType)
        {
            var fundingsourceList = new List<SelectListModel>()
                {
                    new SelectListModel { id = 1, value = "Internal Source" },
                    new SelectListModel { id = 2, value = "Other Source" }
                };
            return fundingsourceList;
        }
        public List<SelectListModel> GetAuditorChecker(int? AuditCheckerId)
        {
            var auditcheckerList = new List<SelectListModel>()
                {
                    new SelectListModel { id = 1, value = "Audit Checker" },
                    new SelectListModel { id = 2, value = "Audit Checker-1" }
                };
            return auditcheckerList;
        }
        public List<SelectListModel> GetAuditorMaker(int? AuditMakerId)
        {
            var auditmakerList = new List<SelectListModel>()
                {
                    new SelectListModel { id = 1, value = "Audit Maker" },
                    new SelectListModel { id = 2, value = "AuditMaker-1" }
                };
            return auditmakerList;
        }
        public IEnumerable<SelectListModel> GetProgramManagers(int? ProgramManagerId)
        {
            var dtoProgram = _genericRepository.Get<DTOModel.User>(x => ProgramManagerId.HasValue ? x.UserId == ProgramManagerId.Value : (1 > 0) && x.IsActive == true);
            var programtList = _mapper.Map<List<SelectListModel>>(dtoProgram);
            return programtList.ToList();
        }
        public IEnumerable<SelectListModel> GetProjects(int? projectId)
        {
            var dtoProject = _genericRepository.Get<DTOModel.Project>(x => projectId.HasValue ? x.ProjectId == projectId.Value : (1 > 0) && x.IsActive == true);
            var projectList = _mapper.Map<List<SelectListModel>>(dtoProject);
            return projectList.ToList();
        }
        public IEnumerable<SelectListModel> GetAuditors(int? auditorId)
        {
            var dtoAuditor = _genericRepository.Get<DTOModel.Auditor>(x => auditorId.HasValue ? x.AuditorId == auditorId.Value : (1 > 0) && x.IsActive == true);
            var auditorList = _mapper.Map<List<SelectListModel>>(dtoAuditor);
            return auditorList.ToList();
        }

        public IEnumerable<SelectListModel> GetStateAsync(int? countryId, int? stateId)
        {
            var dtoDist = _genericRepository.Get<DTOModel.State>(x => stateId.HasValue ? x.StateId == stateId : (1 > 0) && x.IsActive == true);
            var districtList = _mapper.Map<List<SelectListModel>>(dtoDist);
            return districtList.ToList();

        }

        public IEnumerable<SelectListModel> GetAllEmails()
        {
            var uEmails = _genericRepository.Get<DTOModel.User>(u => u.IsActive == true && u.IsDeleted == false).Select(x => new SelectListModel() { id = x.UserId, value = x.EmailId }).ToList();

            var cEmails = _genericRepository.Get<DTOModel.Customer>(c => c.IsDeleted == false).Select(x => new SelectListModel() { id = x.CustomerId, value = x.Email }).ToList();

            uEmails.AddRange(cEmails);

            return uEmails.OrderBy(x => x.value);
        }

        public IEnumerable<SelectListModel> GetRole(int? roleid)
        {
            var dtoRole = _genericRepository.Get<DTOModel.Role>(x => roleid.HasValue ? x.RoleId == roleid.Value : (1 > 0) && x.IsActive == true);
            var roleList = _mapper.Map<List<SelectListModel>>(dtoRole);
            return roleList.ToList();

        }

        public IEnumerable<SelectListModel> GetDistrict(int? stateId, int? districtId)
        {
            try
            {

                var dtoDistrict = _genericRepository.Get<DTOModel.District>(x => (stateId.HasValue ? x.StateId == stateId.Value : (1 > 0)) && (districtId.HasValue ? x.DistrictId == districtId.Value : (1 > 0)) && x.IsActive == true);
                var districtList = _mapper.Map<List<SelectListModel>>(dtoDistrict);
                return districtList.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<SelectListModel> GetBlock(int? stateId, int? districtId, int? blockId)
        {
            try
            {

                var dtoBlock = _genericRepository.Get<DTOModel.Block>(x => (stateId.HasValue ? x.StateId == stateId.Value : (1 > 0)) && (districtId.HasValue ? x.DistrictId == districtId.Value : (1 > 0)) && (blockId.HasValue ? x.BlockId == blockId.Value : (1 > 0)) && x.IsActive == true);
                var blockList = _mapper.Map<List<SelectListModel>>(dtoBlock);
                return blockList.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<SelectListModel> GetTheme(int? themeId)
        {
            try
            {
                var theme = _genericRepository.Get<DTOModel.Theme>(x => (themeId.HasValue ? x.ThemeId == themeId.Value : (1 > 0)) && x.IsActive == true);
                var themeList = _mapper.Map<List<SelectListModel>>(theme);
                return themeList.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<SelectListModel> GetSubTheme(int? themeId, int? subThemeId)
        {
            try
            {
                var subTheme = _genericRepository.Get<DTOModel.SubTheme>(x => (themeId.HasValue ? x.ThemeId == themeId.Value : (1 > 0)) && (subThemeId.HasValue ? x.SubThemeId == subThemeId.Value : (1 > 0)) && x.IsActive == true);
                var subThemeList = _mapper.Map<List<SelectListModel>>(subTheme);
                return subThemeList.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<SelectListModel> GetActivity(int? themeId, int? activityId)
        {
            try
            {
                var activity = _genericRepository.Get<DTOModel.Activity>(x => (themeId.HasValue ? x.ThemeId == themeId.Value : (1 > 0)) && (activityId.HasValue ? x.ActivityId == activityId.Value : (1 > 0)) && x.IsActive == true);

                var activityList = _mapper.Map<List<SelectListModel>>(activity);
                return activityList.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<SelectListModel> GetSubActivity(int? themeId, int? activityId)
        {
            try
            {
                var subActivity = _genericRepository.Get<DTOModel.SubActivity>(x => (themeId.HasValue ? x.ThemeId == themeId.Value : (1 > 0)) && (activityId.HasValue ? x.ActivityId == activityId.Value : (1 > 0)) && x.IsActive == true);

                var subActivityList = _mapper.Map<List<SelectListModel>>(subActivity);
                return subActivityList.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<SelectListModel> GetUoM(int? uomId)
        {
            try
            {
                var uoms = _genericRepository.Get<DTOModel.Uom>(x => (uomId.HasValue ? x.Uomid == uomId.Value : (1 > 0)) && x.IsActive == true);
                var uomList = _mapper.Map<List<SelectListModel>>(uoms);
                return uomList.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<SelectListModel> GetIndicatorResponseType()
        {
            try
            {
                var indicatorResponseTypes = _genericRepository.Get<DTOModel.IndicatorResponseType>(x => x.IsActive == true);
                var selectLists = _mapper.Map<List<SelectListModel>>(indicatorResponseTypes);
                return selectLists.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<SelectListModel> GetIndicatorType()
        {
            try
            {
                var indicatorTypes = _genericRepository.Get<DTOModel.IndicatorType>(x => x.IsActive == true);
                var selectLists = _mapper.Map<List<SelectListModel>>(indicatorTypes);
                return selectLists.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<SelectListModel> GetDepartments()
        {
            var departments = _genericRepository.Get<DTOModel.Department>(x => x.IsActive == true);
            var selectLists = _mapper.Map<List<SelectListModel>>(departments);
            return selectLists.ToList();

        }
        public IEnumerable<SelectListModel> GetPartnerPolicy()
        {
            var partnerpolicy = _genericRepository.Get<DTOModel.PartnerPolicy>(x => x.IsActive == true);
            var selectLists = _mapper.Map<List<SelectListModel>>(partnerpolicy);
            return selectLists.ToList();

        }
        public IEnumerable<SelectListModel> GetDesignations(int? designationId)
        {
            var dtoRole = _genericRepository.Get<DTOModel.Designation>(x => designationId.HasValue ? x.DesignationId == designationId.Value : (1 > 0) && x.IsActive == true);
            var roleList = _mapper.Map<List<SelectListModel>>(dtoRole);
            return roleList.ToList();
        }

        public IEnumerable<SelectListModel> GetPartners(int? partnerId)
        {
            var dtoRole = _genericRepository.Get<DTOModel.Partner>(x => partnerId.HasValue ? x.PartnerId == partnerId.Value : (1 > 0) && x.IsActive == true);
            var roleList = _mapper.Map<List<SelectListModel>>(dtoRole);
            return roleList.ToList();
        }

        public IEnumerable<SelectListModel> GetUsers(int? roleId)
        {
            var users = _genericRepository.Get<DTOModel.User>(x => roleId.HasValue ? x.RoleId == roleId.Value : (1 > 0) && x.IsActive == true);
            var listModels = _mapper.Map<List<SelectListModel>>(users);
            return listModels.ToList();
        }

        public IEnumerable<SelectListModel> GetProcess(int? processId)
        {
            var dtoProcess = _genericRepository.Get<DTOModel.Process>(x => processId.HasValue ? x.ProcessId == processId.Value : (1 > 0) && x.IsActive == true);
            var processList = _mapper.Map<List<SelectListModel>>(dtoProcess);
            return processList.ToList();
        }

        public IEnumerable<SelectListModel> GetModule(int? moduleId)
        {
            var modules = _genericRepository.Get<DTOModel.AuditReviewModule>(x => moduleId.HasValue ? x.ModuleId == moduleId.Value : (1 > 0) && x.IsActive == true);
            var processList = _mapper.Map<List<SelectListModel>>(modules);
            return processList.ToList();
        }

        public IEnumerable<SelectListModel> GetFYYear(int startYear = 2018)
        {
            var fyYears = new List<SelectListModel>();
            for (int year = startYear; year <= DateTime.UtcNow.Year; year++)
            {
                fyYears.Add(new SelectListModel { id = year, value = "FY - " + year + "-" + (year + 1) });
            }
            return fyYears;
        }

        public IEnumerable<SelectListModel> GetFundingAgency(int? agencyType)
        {
            var agencies = _genericRepository.Get<DTOModel.FundingAgency>(x => agencyType.HasValue ? x.AgencyType == agencyType.Value : (1 > 0) && x.IsActive == true);
            var processList = _mapper.Map<List<SelectListModel>>(agencies);
            return processList.ToList();
        }

        public IEnumerable<SelectListModel> GetSource(int? type)
        {
            var source = new List<SelectListModel>();
            for (int i = 1; i <= 5; i++)
            {
                source.Add(new SelectListModel { id = i, value = "Source - " + (i) });
            }
            return source;
        }

        #region Location

        public async Task<IEnumerable<ProjectLocationDetail>> GetStateLocationAsync(int? stateId)
        {
            try
            {
                var states = await _genericRepository.GetAsync<DTOModel.State>(x => (stateId.HasValue ? x.StateId == stateId.Value : (1 > 0)));
                var locations = _mapper.Map<List<ProjectLocationDetail>>(states);
                return locations.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProjectLocationDetail>> GetDistrictLocationAsync(int? stateId, int? districtId)
        {
            try
            {
                var districts = await _genericRepository.GetAsync<DTOModel.District>(x => (stateId.HasValue ? x.StateId == stateId.Value : (1 > 0)) && (districtId.HasValue ? x.DistrictId == districtId.Value : (1 > 0)) && x.IsActive == true);
                var locations = _mapper.Map<List<ProjectLocationDetail>>(districts);
                return locations.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
