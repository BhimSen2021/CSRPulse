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
    public class ActivityService : BaseService, IActivityService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        public ActivityService(IGenericRepository generic, IMapper mapper)
        {
            _genericRepository = generic;
            _mapper = mapper;
        }

        public async Task<Activity> CreateActivityAsync(Activity activity)
        {
            try
            {
                var model = _mapper.Map<DTOModel.Activity>(activity);

                var IsExist = _genericRepository.Exists<DTOModel.Activity>(x => x.ActivityName.ToLower() == activity.ActivityName.ToLower() && x.ThemeId == activity.ThemeId);
                if (!IsExist)
                {
                    var id = await _genericRepository.InsertAsync(model);
                    activity.ThemeId = id;
                }
                activity.IsExist = IsExist;
                return activity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Activity> GetActivityByIdAsync(int activityId)
        {
            try
            {
                var result = await _genericRepository.GetByIDAsync<DTOModel.Activity>(activityId);
                if (result != null)
                    return _mapper.Map<Activity>(result);
                else
                    return new Activity();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Activity>> GetActivityAsync()
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.Activity>();
                return _mapper.Map<List<Activity>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<Activity>> GetActivityAsync(Activity activity)
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.Activity>(x => x.ThemeId == activity.ThemeId && (activity.IsActiveFilter.HasValue ? x.IsActive == activity.IsActiveFilter.Value : 1 > 0));
                return _mapper.Map<List<Activity>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateActivityAsync(Activity activity)
        {
            try
            {
                var IsExist = _genericRepository.Exists<DTOModel.Activity>(x => x.ActivityName.ToLower() == activity.ActivityName.ToLower() && x.ActivityId != activity.ActivityId && x.ThemeId != activity.ThemeId);

                activity.IsExist = IsExist;

                if (!IsExist)
                {
                    var result = await _genericRepository.GetByIDAsync<DTOModel.Activity>(activity.ActivityId);
                    if (result != null)
                    {
                        result.ThemeId = activity.ThemeId;
                        result.ActivityName = activity.ActivityName;
                        result.ActivityCode = activity.ActivityCode;
                        result.IsActive = activity.IsActive;
                        result.UpdatedOn = activity.UpdatedOn;
                        result.UpdatedBy = activity.UpdatedBy;
                        result.UpdatedRid = activity.UpdatedRid;
                        result.UpdatedRname = activity.UpdatedRname;
                        _genericRepository.Update(result);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ActiveDeActive(int id, bool isActive)
        {
            try
            {
                var model = _genericRepository.GetByID<DTOModel.Activity>(id);
                model.IsActive = isActive;
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
