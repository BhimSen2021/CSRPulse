using CSRPulse.Data.Repositories;
using CSRPulse.Model;
using CSRPulse.Model.Admin;
using DTOModel = CSRPulse.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace CSRPulse.Services.Admin
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
        public async Task<int> AddNewPlanAsync(Plan plan)
        {
            try
            {
               var pplan= _mapper.Map<DTOModel.Plan>(plan);                
                 await _genericRepository.InsertAsync(pplan);            

                return pplan.PlanId;
            }
            catch (Exception)
            {               
                throw;
            }
        }
               
        public async Task<List<Model.Plan>> GetAllPlanAsync()
        {
            IEnumerable<DTOModel.Plan> result = await _genericRepository.GetAsync<DTOModel.Plan>();
            try
            {
                return _mapper.Map<List<Model.Plan>>(result);
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<Model.Plan> GetPlanByIdAsync()
        {
            throw new NotImplementedException();
        }
    }
}
