using CSRPulse.Data.Repositories;
using CSRPulse.Model;
using CSRPulse.Model.Admin;
using DTOModel = CSRPulse.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace CSRPulse.Services.Admin.Plan
{
    public class PlanService : IPlanService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        public PlanService(IGenericRepository genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }
        public async Task<int> AddNewPlanAsync(PlanModel planModel)
        {
            try
            {
                //var plan = new DTOModel.Plan()
                //{
                //    PlanName = planModel.PlanName,
                //    PlanDetail = planModel.PlanDetail,
                //    CreatedOn = DateTime.UtcNow,
                //    CreatedBy = 1,
                //    IsDeleted = false
                //};

               var plan= _mapper.Map<DTOModel.Plan>(planModel);
                return await _genericRepository.InsertAsync(plan);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> AddNewUserTypeAsync(UserTypeModel uTypeModel)
        {
            try
            {
                var dtoType = _mapper.Map<DTOModel.UserType>(uTypeModel);
                await _genericRepository.InsertAsync(dtoType);
                uTypeModel.userModel = new UserModel();
                uTypeModel.userModel.UserTypeId = dtoType.UserTypeID;
                uTypeModel.userModel.UserName ="A" ;
                uTypeModel.userModel.Password ="12356" ;
                uTypeModel.userModel.EmailId = "12356";
                uTypeModel.userModel.FullName ="AAAA" ;
                uTypeModel.userModel.IsActive =true ;
                uTypeModel.userModel.IsDeleted = false;
                uTypeModel.userModel.CreatedBy =1 ;
                uTypeModel.userModel.CreatedOn = DateTime.Now;
                var dtouser = _mapper.Map<DTOModel.User>(uTypeModel.userModel);
                await _genericRepository.InsertAsync(dtouser);
                return dtouser.UserTypeID;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<PlanModel>> GetAllPlanAsync()
        {
            IEnumerable<DTOModel.Plan> result = await _genericRepository.GetAsync<DTOModel.Plan>();
            try
            {
                return _mapper.Map<List<PlanModel>>(result);
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<PlanModel> GetPlanByIdAsync()
        {
            throw new NotImplementedException();
        }
    }
}
