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
namespace CSRPulse.Services
{
    public class IndicatorService : BaseService, IIndicatorService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        public IndicatorService(IGenericRepository generic, IMapper mapper)
        {
            _genericRepository = generic;
            _mapper = mapper;
        }

        public async Task<Indicator> CreateIndicatorAsync(Indicator indicator)
        {
            try
            {
                var model = _mapper.Map<DTOModel.Indicator>(indicator);

                var IsExist = _genericRepository.Exists<DTOModel.Indicator>(x => x.IndicatorName.ToLower() == indicator.IndicatorName.ToLower() && x.ThemeId == indicator.ThemeId && x.ActivityId == indicator.ActivityId);
                if (!IsExist)
                {
                    var id = await _genericRepository.InsertAsync(model);
                    indicator.IndicatorId = id;
                }
                indicator.IsExist = IsExist;
                return indicator;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Indicator> GetIndicatorIdAsync(int indicatorId)
        {
            try
            {
                var indicator = new Indicator();
                var result = await _genericRepository.GetByIDAsync<DTOModel.Indicator>(indicatorId);

                if (result != null)
                {
                    indicator = _mapper.Map<Indicator>(result);                  
                }

                return indicator;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Indicator>> GetIndicatorAsync()
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.Indicator>();
                return _mapper.Map<List<Indicator>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<Indicator>> GetIndicatorAsync(Indicator indicator)
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.Indicator>(x => x.ThemeId == indicator.ThemeId && x.ActivityId == indicator.ActivityId && (indicator.IsActiveFilter.HasValue ? x.IsActive == indicator.IsActiveFilter.Value : 1 > 0));
                return _mapper.Map<List<Indicator>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateIndicatorAsync(Indicator activity)
        {
            try
            {
                var IsExist = _genericRepository.Exists<DTOModel.Indicator>(x => x.IndicatorName.ToLower() == activity.IndicatorName.ToLower() && x.ActivityId != activity.ActivityId && x.ThemeId != activity.ThemeId);

                activity.IsExist = IsExist;

                if (!IsExist)
                {
                    var result = await _genericRepository.GetByIDAsync<DTOModel.Indicator>(activity.ActivityId);
                    if (result != null)
                    {
                        result.ThemeId = activity.ThemeId;
                        result.SubThemeId = activity.SubThemeId;
                        result.ActivityId = activity.ActivityId;
                        result.SubActivityId = activity.SubActivityId;
                        result.Uomid = activity.UomId;
                        result.IndicatorType = activity.IndicatorType;
                        result.ResponseType = activity.ResponseType;
                        result.FrequencyOfReporting = activity.FrequencyOfReporting;
                        result.IsCumulative = activity.IsCumulative;
                        result.KeyIndicator = activity.KeyIndicator;
                        result.IndicatorName = activity.IndicatorName;
                        result.IndicatorShortName = activity.IndicatorShortName;
                        result.IndicatorCode = activity.IndicatorCode;
                        result.Description = activity.Description;
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
                var model = _genericRepository.GetByID<DTOModel.Indicator>(id);
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
