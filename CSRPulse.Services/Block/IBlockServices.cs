using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSRPulse.Model;
namespace CSRPulse.Services
{
  public interface IBlockServices
    {
        Task<List<Model.Block>> GetBlockList(Model.Block block);
        Task<bool> CreateBlock(Model.Block block);
        Task<bool> UpdateBlock(Model.Block block);
        Task<Model.Block> GetBlockById(int blockId);
        Task<bool> RecordExist(Model.Block block);
        bool ActiveDeActive(int id, bool IsActive);
    }
}
