using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public interface IThematicService
    {
        Task<int> AddNewAsync<T>(T model) where T : class;
        Task UpdateAsync<T>(T model) where T : class;
        Task DeleteAsync<T>(T model) where T : class;
        Task<List<T>> GetAllAsync<T>() where T : class;
        Task<T> GetByIdAsync<T>(int Id) where T : class;
    }
}
