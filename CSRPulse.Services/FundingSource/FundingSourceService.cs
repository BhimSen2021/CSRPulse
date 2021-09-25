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
    public class FundingSourceService : BaseService, IFundingSourceService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;


        public FundingSourceService(IGenericRepository generic, IMapper mapper)
        {
            _genericRepository = generic;
            _mapper = mapper;
        }

        public async Task<FundingSource> CreateFundingSourceAsync(FundingSource FundingSource)
        {
            try
            {
                var model = _mapper.Map<DTOModel.FundingSource>(FundingSource);

                var IsExist = _genericRepository.Exists<DTOModel.FundingSource>(x => x.SourceName.ToLower() == FundingSource.SourceName.ToLower());
                if (!IsExist)
                {
                    var id = await _genericRepository.InsertAsync(model);
                    FundingSource.SourceId = id;
                }
                FundingSource.IsExist = IsExist;
                return FundingSource;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<FundingSource> GetFundingSourceByIdAsync(int SourceId)
        {
            try
            {
                var result = await _genericRepository.GetByIDAsync<DTOModel.FundingSource>(SourceId);
                if (result != null)
                    return _mapper.Map<FundingSource>(result);
                else
                    return new FundingSource();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<FundingSource>> GetFundingSourcesAsync()
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.FundingSource>();
                return _mapper.Map<List<FundingSource>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<FundingSource>> GetFundingSourcesAsync(FundingSource FundingSource)
        {
            try
            {
                var result = await Task.FromResult(_genericRepository.GetIQueryable<DTOModel.FundingSource>(x =>
                (FundingSource.IsActiveFilter.HasValue ? x.IsActive == FundingSource.IsActiveFilter.Value : 1 > 0) 
                 && (FundingSource.FilterSourceType.HasValue ? x.SourceType == FundingSource.FilterSourceType.Value : 1 > 0)));
                
                return _mapper.Map<List<FundingSource>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateFundingSourceAsync(FundingSource FundingSource)
        {
            try
            {
                var IsExist = _genericRepository.Exists<DTOModel.FundingSource>(x => x.SourceName.ToLower() == FundingSource.SourceName.ToLower() && x.SourceId != FundingSource.SourceId);

                FundingSource.IsExist = IsExist;

                if (!IsExist)
                {
                    var result = await _genericRepository.GetByIDAsync<DTOModel.FundingSource>(FundingSource.SourceId);
                    if (result != null)
                    {
                        result.SourceName = FundingSource.SourceName;
                        result.Sequence = FundingSource.Sequence.Value;
                        result.SourceType = FundingSource.SourceType.Value;
                        result.IsActive = FundingSource.IsActive;
                        result.UpdatedOn = FundingSource.UpdatedOn;
                        result.UpdatedBy = FundingSource.UpdatedBy;
                        result.UpdatedRid = FundingSource.UpdatedRid;
                        result.UpdatedRname = FundingSource.UpdatedRname;
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
                var model = _genericRepository.GetByID<DTOModel.FundingSource>(id);
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
