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
    public class SubThemeService : BaseService, ISubThemeService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;

        public SubThemeService(IGenericRepository generic, IMapper mapper)
        {
            _genericRepository = generic;
            _mapper = mapper;
        }
        public async Task<SubTheme> CreateSubThemeAsync(SubTheme theme)
        {
            try
            {
                var model = _mapper.Map<DTOModel.SubTheme>(theme);

                var IsExist = _genericRepository.Exists<DTOModel.SubTheme>(x => x.SubThemeName.ToLower() == theme.SubThemeName.ToLower() && x.ThemeId == theme.ThemeId);
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

        public async Task<SubTheme> GetSubThemeByIdAsync(int SubThemeId)
        {
            try
            {
                var result = await _genericRepository.GetByIDAsync<DTOModel.SubTheme>(SubThemeId);
                if (result != null)
                    return _mapper.Map<SubTheme>(result);
                else
                    return new SubTheme();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<SubTheme>> GetSubThemesAsync()
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.SubTheme>();
                return _mapper.Map<List<SubTheme>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<SubTheme>> GetSubThemesAsync(SubTheme subTheme)
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.SubTheme>(x => x.ThemeId == subTheme.ThemeId && (subTheme.IsActiveFilter.HasValue ? x.IsActive == subTheme.IsActiveFilter.Value : 1 > 0));
                return _mapper.Map<List<SubTheme>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateSubThemeAsync(SubTheme theme)
        {
            try
            {
                var IsExist = _genericRepository.Exists<DTOModel.SubTheme>(x => x.SubThemeName.ToLower() == theme.SubThemeName.ToLower() && x.SubThemeId != theme.SubThemeId && x.ThemeId != theme.ThemeId);

                theme.IsExist = IsExist;

                if (!IsExist)
                {
                    var result = await _genericRepository.GetByIDAsync<DTOModel.SubTheme>(theme.SubThemeId);
                    if (result != null)
                    {
                        result.ThemeId = theme.ThemeId;
                        result.SubThemeName = theme.SubThemeName;
                        result.SubThemeCode = theme.SubThemeCode;
                        result.IsActive = theme.IsActive;
                        result.UpdatedOn = theme.UpdatedOn;
                        result.UpdatedBy = theme.UpdatedBy;
                        result.UpdatedRid = theme.UpdatedRid;
                        result.UpdatedRname = theme.UpdatedRname;
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
                var model = _genericRepository.GetByID<DTOModel.SubTheme>(id);
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
