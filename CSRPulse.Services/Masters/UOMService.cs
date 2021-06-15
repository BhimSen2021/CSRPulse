using AutoMapper;
using CSRPulse.Data.Repositories;
using CSRPulse.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DTOModel = CSRPulse.Data.Models;

namespace CSRPulse.Services
{
    public class UOMService : BaseService, IUOMService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;

        public UOMService(IGenericRepository generic, IMapper mapper)
        {
            _genericRepository = generic;
            _mapper = mapper;
        }
        public async Task<Uom> CreateUOMAsync(Uom uom)
        {
            try
            {
                var model = _mapper.Map<DTOModel.Uom>(uom);

                var IsExist = _genericRepository.Exists<DTOModel.Uom>(x => x.UnitName.ToLower() == uom.UnitName.ToLower());
                if (!IsExist)
                {
                    var res = await _genericRepository.InsertAsync(model);
                    uom.UOMId = res;

                }
                uom.IsExist = IsExist;

                return uom;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Uom> GetUOMByIdAsync(int UOMId)
        {
            try
            {
                var result = await _genericRepository.GetByIDAsync<DTOModel.Uom>(UOMId);
                if (result != null)
                    return _mapper.Map<Uom>(result);
                else
                    return new Uom();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Uom>> GetUOMsAsync()
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.Uom>();
                return _mapper.Map<List<Uom>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateUOMAsync(Uom uom)
        {
            try
            {
                var IsExist = _genericRepository.Exists<DTOModel.Uom>(x => x.UnitName.ToLower() == uom.UnitName.ToLower() && x.Uomid != uom.UOMId);

                uom.IsExist = IsExist;

                if (!IsExist)
                {
                    var result = await _genericRepository.GetByIDAsync<DTOModel.Uom>(uom.UOMId);
                    if (result != null)
                    {
                        result.UnitName = uom.UnitName;
                        result.IsActive = uom.IsActive;
                        result.UpdatedOn = uom.UpdatedOn;
                        result.UpdatedBy = uom.UpdatedBy;
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
    }
}
