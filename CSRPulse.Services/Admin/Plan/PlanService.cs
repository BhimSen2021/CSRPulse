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

        public Task<int> AddNewUserTypeAsync(UserTypeModel uTypeModel)
        {
            throw new NotImplementedException();
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
