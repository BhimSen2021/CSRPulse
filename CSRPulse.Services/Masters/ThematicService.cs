using AutoMapper;
using CSRPulse.Data.Repositories;
using System;
using DTOModel = CSRPulse.Data.Models;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSRPulse.Model;

namespace CSRPulse.Services
{
    public class ThematicService : IThematicService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        public ThematicService(IGenericRepository genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }
        public async Task<int> AddNewAsync<T>(T model) where T : class
        {
            try
            {
                var res = CreateMap<T>(model);
                return await _genericRepository.InsertAsync(res);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task DeleteAsync<T>(T model) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAllAsync<T>() where T : class
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<T> GetByIdAsync<T>(int Id) where T : class
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync<T>(T model) where T : class
        {
            throw new NotImplementedException();
        }

        private dynamic CreateMap<T>(T model)
        {
            if (model is Uom)
                return _mapper.Map<DTOModel.Uom>(model);
            else if (model is Activity)
                return _mapper.Map<DTOModel.Activity>(model);
            else if (model is SubActivity)
                return _mapper.Map<DTOModel.SubActivity>(model);
            else if (model is Theme)
                return _mapper.Map<DTOModel.Theme>(model);
            else if (model is SubTheme)
                return _mapper.Map<DTOModel.SubTheme>(model);
            else if (model is Indicator)
                return _mapper.Map<DTOModel.Indicator>(model);

            else
                return null;

        }
       
    }
}
