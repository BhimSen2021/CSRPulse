using AutoMapper;
using CSRPulse.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOModel = CSRPulse.Data.Models;
namespace CSRPulse.Services
{
    public class StateServices : BaseService, IStateServices
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        public StateServices(IGenericRepository generic, IMapper mapper)
        {
            _genericRepository = generic;
            _mapper = mapper;
        }
        public async Task<bool> CreateState(Model.State state)
        {
            try
            {
                var dtoState = _mapper.Map<DTOModel.State>(state);

                var res = await _genericRepository.InsertAsync(dtoState);
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

        public async Task<List<Model.State>> GetStateList()
        {
            try
            {
                var getStates = await _genericRepository.GetAsync<DTOModel.State>();
                var stateList = _mapper.Map<List<Model.State>>(getStates);
                return stateList;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Model.State> GetStateById(int stateId)
        {
            try
            {
                var getStates = await _genericRepository.GetByIDAsync<DTOModel.State>(stateId);
                if (getStates != null)
                    return _mapper.Map<Model.State>(getStates);
                else
                    return new Model.State();

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> UpdateState(Model.State state)
        {
            try
            {

                var getStates = await _genericRepository.GetByIDAsync<DTOModel.State>(state.StateId);
                if (getStates != null)
                {
                    if (getStates.StateName == state.StateName && getStates.StateCode == state.StateCode && getStates.StateShort == state.StateShort)
                        return true;

                    getStates.StateName = state.StateName;
                    getStates.StateShort = state.StateShort;
                    getStates.StateCode = state.StateCode;
                    getStates.UpdatedBy = state.UpdatedBy;
                    getStates.UpdatedOn = state.UpdatedOn;
                    _genericRepository.Update(getStates);
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

        public async Task<bool> RecordExist(Model.State state)
        {
            try
            {
                return await _genericRepository.ExistsAsync<DTOModel.State>(x => x.StateName == state.StateName || x.StateCode == state.StateCode);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Model.State>> GetStatesAsync(Model.State state)
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.State>(x => (state.IsActiveFilter.HasValue ? x.IsActive == state.IsActiveFilter.Value : 1 > 0));
                return _mapper.Map<List<Model.State>>(result);
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
                var model = _genericRepository.GetByID<DTOModel.State>(id);
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
