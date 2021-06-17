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
    public class ThemeService : BaseService, IThemeService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;

        public ThemeService(IGenericRepository generic, IMapper mapper)
        {
            _genericRepository = generic;
            _mapper = mapper;
        }

        public async Task<Theme> CreateThemeAsync(Theme theme)
        {
            try
            {
                var model = _mapper.Map<DTOModel.Theme>(theme);

                var IsExist = _genericRepository.Exists<DTOModel.Theme>(x => x.ThemeName.ToLower() == theme.ThemeName.ToLower());
                if (!IsExist)
                {
                    var id = await _genericRepository.InsertAsync(model);
                    theme.ThemeId = id;
                }
                theme.IsExist = IsExist;
                return theme;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Theme> GetThemeByIdAsync(int ThemeId)
        {
            try
            {
                var result = await _genericRepository.GetByIDAsync<DTOModel.Theme>(ThemeId);
                if (result != null)
                    return _mapper.Map<Theme>(result);
                else
                    return new Theme();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Theme>> GetThemesAsync()
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.Theme>();
                return _mapper.Map<List<Theme>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateThemeAsync(Theme theme)
        {
            try
            {
                var IsExist = _genericRepository.Exists<DTOModel.Theme>(x => x.ThemeName.ToLower() == theme.ThemeName.ToLower() && x.ThemeId != theme.ThemeId);

                theme.IsExist = IsExist;

                if (!IsExist)
                {
                    var result = await _genericRepository.GetByIDAsync<DTOModel.Theme>(theme.ThemeId);
                    if (result != null)
                    {
                        result.ThemeName = theme.ThemeName;
                        result.ThemeCode = theme.ThemeCode;
                        result.IsActive = theme.IsActive;
                        result.UpdatedOn = theme.UpdatedOn;
                        result.UpdatedBy = theme.UpdatedBy;
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
        public bool ActiveDeActive(int id, bool IsActive)
        {
            try
            {
                var model = _genericRepository.GetByID<DTOModel.Theme>(id);
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
