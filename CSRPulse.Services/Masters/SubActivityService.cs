﻿using AutoMapper;
using CSRPulse.Data.Repositories;
using CSRPulse.Model;
using DTOModel = CSRPulse.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace CSRPulse.Services
{
    public class SubActivityService : BaseService, ISubActivityService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        public SubActivityService(IGenericRepository generic, IMapper mapper)
        {
            _genericRepository = generic;
            _mapper = mapper;
        }

        public async Task<SubActivity> CreateSubActivityAsync(SubActivity subActivity)
        {
            try
            {
                var model = _mapper.Map<DTOModel.SubActivity>(subActivity);

                var IsExist = _genericRepository.Exists<DTOModel.SubActivity>(x => x.SubActivityName.ToLower() == subActivity.SubActivityName.ToLower() && x.ThemeId == subActivity.ThemeId && x.ActivityId == subActivity.ActivityId);
                if (!IsExist)
                {
                    var id = await _genericRepository.InsertAsync(model);
                    subActivity.SubActivityId = id;
                }
                subActivity.IsExist = IsExist;
                return subActivity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<SubActivity> GetSubActivityIdAsync(int subActivityId)
        {
            try
            {
                var result = await _genericRepository.GetByIDAsync<DTOModel.SubActivity>(subActivityId);
                if (result != null)
                    return _mapper.Map<SubActivity>(result);
                else
                    return new SubActivity();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<SubActivity>> GetSubActivityAsync()
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.SubActivity>();
                return _mapper.Map<List<SubActivity>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<SubActivity>> GetSubActivityAsync(int themeId, int activityId)
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.SubActivity>(x => x.ThemeId == themeId && x.ActivityId == activityId);
                return _mapper.Map<List<SubActivity>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateSubActivityAsync(SubActivity activity)
        {
            try
            {
                var IsExist = _genericRepository.Exists<DTOModel.SubActivity>(x => x.SubActivityName.ToLower() == activity.SubActivityName.ToLower() && x.ActivityId != activity.ActivityId && x.ThemeId != activity.ThemeId);

                activity.IsExist = IsExist;

                if (!IsExist)
                {
                    var result = await _genericRepository.GetByIDAsync<DTOModel.SubActivity>(activity.ActivityId);
                    if (result != null)
                    {
                        result.ThemeId = activity.ThemeId;
                        result.SubActivityName = activity.SubActivityName;
                        result.SubActivityCode= activity.SubActivityCode;
                        result.IsActive = activity.IsActive;
                        result.UpdatedOn = activity.UpdatedOn;
                        result.UpdatedBy = activity.UpdatedBy;
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
