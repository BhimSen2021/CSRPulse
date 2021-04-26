using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CSRPulse.Data.Repositories
{
    public interface IGenericRepository
    {
        IEnumerable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "") where TEntity : class;
        Task<IEnumerable<TEntity>> GetAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "") where TEntity : class;
        IEnumerable<TEntity> GetReportResult<TEntity>(int pageNum, int pageSize, out int rowsCount, Expression<Func<TEntity, bool>> filter = null, string sortOn = "", bool isAscendingOrder = false, string includeProperties = "") where TEntity : class;
        IEnumerable<TEntity> GetPagedResult<TEntity>(int pageNum, int pageSize, out int rowsCount, Expression<Func<TEntity, bool>> filter = null, string sortOn = "", bool isAscendingOrder = false, string includeProperties = "") where TEntity : class;
        Tuple<IEnumerable<TEntity>, int> GetPagedResultAsync<TEntity>(int pageNum, int pageSize, Expression<Func<TEntity, bool>> filter = null, string sortOn = "", bool isAscendingOrder = false, string includeProperties = "") where TEntity : class;
        TEntity GetByID<TEntity>(object id) where TEntity : class;
        Task<TEntity> GetByIDAsync<TEntity>(object id) where TEntity : class;
        TEntity GetFirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class;
        Task<TEntity> GetFirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class;
        bool Exists<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class;
        Task<bool> ExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class;
        int Insert<TEntity>(TEntity entity) where TEntity : class;
        Task<int> InsertAsync<TEntity>(TEntity entity) where TEntity : class;
        bool AddMultipleEntity<TEntity>(IEnumerable<TEntity> entityList) where TEntity : class;
        Task<bool> AddMultipleEntityAsync<TEntity>(IEnumerable<TEntity> entityList) where TEntity : class;
        void Delete<TEntity>(object id) where TEntity : class;
        Task DeleteAsync<TEntity>(object id) where TEntity : class;
        void Delete<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class;
        Task DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class;
        void Delete<TEntity>(TEntity entityToDelete) where TEntity : class;
        Task DeleteAsync<TEntity>(TEntity entityToDelete) where TEntity : class;
        void Update<TEntity>(TEntity entityToUpdate) where TEntity : class;
        Task UpdateAsync<TEntity>(TEntity entityToUpdate) where TEntity : class;
        DbParameter GetParameter();
        Task<DbParameter> GetParameterAsync();

        DbParameter GetParameter(string paramName, System.Data.DbType dbtype, object value = null, bool bOutput = false);
        Task<DbParameter> GetParameterAsync(string paramName, System.Data.DbType dbtype, object value = null, bool bOutput = false);
        void Dispose();
        bool RemoveMultipleEntity<TEntity>(IEnumerable<TEntity> removeEntityList) where TEntity : class;
        Task<bool> RemoveMultipleEntityAsync<TEntity>(IEnumerable<TEntity> removeEntityList) where TEntity : class;
        IQueryable<TEntity> GetIQueryable<TEntity>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "") where TEntity : class;
        Task<IQueryable<TEntity>> GetIQueryableAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "") where TEntity : class;

        //IEnumerable<T> ExecWithStoreProcedure<T>(string query, params object[] parameters);
        //void ExecuteWithStoreProcedure(string query, params object[] parameters);
        //DataTable GetTablesSchema(string sTableName);

        bool SaveChanges();
    }
}
