using AutoMapper;
using CSRPulse.Data.Repositories;
using CSRPulse.Model;
using System;
using System.Collections.Generic;
using System.Text;
using DTOModel = CSRPulse.Data.Models;
using System.Threading.Tasks;
using System.Linq;

namespace CSRPulse.Services
{
    public class MaintenanceService : BaseService, IMaintenanceService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository _genericRepository;


        public MaintenanceService(IMapper mapper, IGenericRepository genericRepository)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
        }

        public Maintenance GetMaintenanceDetails()
        {
            Maintenance maintenance = new Maintenance();
            try
            {
                var result = _genericRepository.Get<DTOModel.Maintenance>().FirstOrDefault();

                maintenance = _mapper.Map<Maintenance>(result);
                return maintenance;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> GoUnderMaintenance(Maintenance maintenance)
        {
            try
            {
                var mData = _genericRepository.Get<DTOModel.Maintenance>().FirstOrDefault();
                if (mData != null)
                {
                    mData.IsMaintenance = true;
                    mData.StartDateTime = maintenance.StartDateTime;
                    mData.EndDateTime = maintenance.EndDateTime;
                    mData.Message = maintenance.Message;
                    _genericRepository.Update(mData);
                    return true;
                }
                else
                {
                    var model = _mapper.Map<DTOModel.Maintenance>(maintenance);
                    await _genericRepository.InsertAsync(model);

                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsUnderMaintenance()
        {
            var mData = _genericRepository.Get<DTOModel.Maintenance>().FirstOrDefault();
            if (mData != null)
            {

                return mData.IsMaintenance;
            }
            return false;
        }

        public void UpdateMaintenance(bool IsMaintenance)
        {
            try
            {
                var mData = _genericRepository.Get<DTOModel.Maintenance>().FirstOrDefault();
                if (mData != null)
                {
                    mData.IsMaintenance = IsMaintenance;
                    _genericRepository.Update(mData);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
