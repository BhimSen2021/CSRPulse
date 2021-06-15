using AutoMapper;
using CSRPulse.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOModel = CSRPulse.Data.Models;
namespace CSRPulse.Services
{
    public class BlockServices : BaseService, IBlockServices
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        public BlockServices(IGenericRepository generic, IMapper mapper)
        {
            _genericRepository = generic;
            _mapper = mapper;
        }
        public async Task<bool> CreateBlock(Model.Block block)
        {
            try
            {
                var dtoBlock = _mapper.Map<DTOModel.Block>(block);

                var res = await _genericRepository.InsertAsync(dtoBlock);
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

        public async Task<List<Model.Block>> GetBlockList(int? stateId, int? districtId)
        {
            try
            {
                var getBlocks = await Task.FromResult(_genericRepository.GetIQueryable<DTOModel.Block>(x => (stateId.HasValue ? x.StateId == stateId : (1 > 0))
                && (districtId.HasValue ? x.DistrictId == districtId : (1 > 0))
                ).Include(s => s.State).Include(d => d.District));
                var disList = _mapper.Map<List<Model.Block>>(getBlocks);
                return disList;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Model.Block> GetBlockById(int blockId)
        {
            try
            {
                var getBlocks = await _genericRepository.GetByIDAsync<DTOModel.Block>(blockId);
                if (getBlocks != null)
                    return _mapper.Map<Model.Block>(getBlocks);
                else
                    return new Model.Block();

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> UpdateBlock(Model.Block block)
        {
            try
            {

                var getBlocks = await _genericRepository.GetByIDAsync<DTOModel.Block>(block.BlockId);
                if (getBlocks != null)
                {
                    if (getBlocks.BlockName == block.BlockName && getBlocks.BlockCode == block.BlockCode && getBlocks.StateId == block.StateId && getBlocks.DistrictId == block.DistrictId)
                        return true;
                    //else
                    //{
                    //    var recordExist = _genericRepository.GetIQueryable<DTOModel.Block>(x => x.BlockName == block.BlockName || x.BlockShort == block.BlockShort || x.BlockCode == block.BlockCode).FirstOrDefault();

                    //    if (recordExist != null)
                    //    {
                    //        block.RecordExist = true;
                    //        return false;
                    //    }
                    //}
                    getBlocks.BlockName = block.BlockName;
                    getBlocks.BlockCode = block.BlockCode;
                    getBlocks.StateId = block.StateId.Value;
                    getBlocks.DistrictId = block.DistrictId.Value;
                    getBlocks.UpdatedBy = block.UpdatedBy;
                    getBlocks.UpdatedOn = block.UpdatedOn;
                    _genericRepository.Update(getBlocks);
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

        public async Task<bool> RecordExist(Model.Block block)
        {
            try
            {
                return await _genericRepository.ExistsAsync<DTOModel.Block>(x => x.BlockName == block.BlockName || x.BlockCode == block.BlockCode);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
