using CSRPulse.Data.Data;
using CSRPulse.Data.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSRPulse.Data.Repositories
{
    public class GenericRepository : IGenericRepository
    {
        protected internal CSRPulseDbContext _dbContext = null;
        public GenericRepository()
        {
            _dbContext = new CSRPulseDbContext();
        }

        public virtual IEnumerable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "") where TEntity : class
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
        public virtual async Task<IEnumerable<TEntity>> GetAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "") where TEntity : class
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }
        public virtual IEnumerable<TEntity> GetReportResult<TEntity>(int pageNum, int pageSize, out int rowsCount, Expression<Func<TEntity, bool>> filter = null, string sortOn = "", bool isAscendingOrder = false, string includeProperties = "") where TEntity : class
        {
            IEnumerable<TEntity> pagedResult;
            rowsCount = 0;
            if (pageSize <= 0) pageSize = 20;
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            rowsCount = query.Count();
            if (rowsCount <= pageSize || pageNum <= 0) pageNum = 1;
            int excludedRows = (pageNum - 1) * pageSize;

            if (!string.IsNullOrEmpty(sortOn))
            {
                var param = Expression.Parameter(typeof(TEntity), "i");

                MemberExpression property = null;
                string[] fieldNames = sortOn.Split('.');
                foreach (string filed in fieldNames)
                {
                    if (property == null)
                    {
                        property = Expression.Property(param, filed);
                    }
                    else
                    {
                        property = Expression.Property(property, filed);
                    }
                }

                Expression conversion = Expression.Convert(property, typeof(object));
                var mySortExpression = Expression.Lambda<Func<TEntity, object>>(conversion, param).Compile();
                pagedResult = isAscendingOrder ? query.OrderBy(mySortExpression).ToList() : query.OrderByDescending(mySortExpression).ToList();
            }
            else
            {
                pagedResult = query.ToList();
            }
            return pagedResult;
        }
        public virtual IEnumerable<TEntity> GetPagedResult<TEntity>(int pageNum, int pageSize, out int rowsCount, Expression<Func<TEntity, bool>> filter = null, string sortOn = "", bool isAscendingOrder = false, string includeProperties = "") where TEntity : class
        {
            int pn = pageNum, ps = pageSize;
            IEnumerable<TEntity> pagedResult;
            rowsCount = 0;
            if (pageSize <= 0) pageSize = 20;
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            rowsCount = query.Count();
            if (rowsCount <= pageSize || pageNum <= 0) pageNum = 1;
            int excludedRows = (pageNum - 1) * pageSize;

            if (!string.IsNullOrEmpty(sortOn))
            {
                var param = Expression.Parameter(typeof(TEntity), "i");

                MemberExpression property = null;
                string[] fieldNames = sortOn.Split('.');
                foreach (string filed in fieldNames)
                {
                    if (property == null)
                    {
                        property = Expression.Property(param, filed);
                    }
                    else
                    {
                        property = Expression.Property(property, filed);
                    }
                }

                Expression conversion = Expression.Convert(property, typeof(object));//Expression.Property(param, fieldName)
                var mySortExpression = Expression.Lambda<Func<TEntity, object>>(conversion, param).Compile();
                pagedResult = isAscendingOrder ? query.OrderBy(mySortExpression).ToList() : query.OrderByDescending(mySortExpression).ToList();
            }
            else
            {
                pagedResult = query.ToList();
            }
            if (pn == 0 && ps == 0)
            {
                return pagedResult;
            }
            else
            {
                return pagedResult.Skip(excludedRows).Take(pageSize);
            }

        }
        public Tuple<IEnumerable<TEntity>, int> GetPagedResultAsync<TEntity>(int pageNum, int pageSize, Expression<Func<TEntity, bool>> filter = null, string sortOn = "", bool isAscendingOrder = false, string includeProperties = "") where TEntity : class
        {
            throw new NotImplementedException();
        }
        public virtual TEntity GetByID<TEntity>(object id) where TEntity : class
        {
            return _dbContext.Set<TEntity>().Find(id);
        }
        public async Task<TEntity> GetByIDAsync<TEntity>(object id) where TEntity : class
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }
        public virtual TEntity GetFirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(filter);
        }
        public async Task<TEntity> GetFirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(filter);
        }
        public virtual bool Exists<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class
        {
            if (filter == null) return false;
            return (_dbContext.Set<TEntity>().Any(filter));
        }
        public async Task<bool> ExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class
        {
            if (filter == null) return false;
            return await (_dbContext.Set<TEntity>().AnyAsync(filter));
        }
        public virtual int Insert<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                _dbContext.Set<TEntity>().Add(entity);
                _dbContext.SaveChanges();
                int ret = 0;
                PropertyInfo key = typeof(TEntity).GetProperties().FirstOrDefault(p => p.CustomAttributes.Any(attr => attr.AttributeType == typeof(KeyAttribute)));

                if (key != null)
                {
                    ret = (int)key.GetValue(entity, null);
                }
                return ret;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }
        public async Task<int> InsertAsync<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                await _dbContext.Set<TEntity>().AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                int ret = 0;
                PropertyInfo key = typeof(TEntity).GetProperties().FirstOrDefault(p => p.CustomAttributes.Any(attr => attr.AttributeType == typeof(KeyAttribute)));

                if (key != null)
                {
                    ret = (int)key.GetValue(entity, null);
                }
                return ret;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }
        public virtual bool AddMultipleEntity<TEntity>(IEnumerable<TEntity> entityList) where TEntity : class
        {
            bool flag;
            if (entityList == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                _dbContext.Set<TEntity>().AddRange(entityList);
             _dbContext.SaveChanges();
                flag = true;
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }
        public async Task<bool> AddMultipleEntityAsync<TEntity>(IEnumerable<TEntity> entityList) where TEntity : class
        {
            bool flag;
            if (entityList == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                await _dbContext.Set<TEntity>().AddRangeAsync(entityList);
                await _dbContext.SaveChangesAsync();
                flag = true;
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }
        public virtual bool RemoveMultipleEntity<TEntity>(IEnumerable<TEntity> removeEntityList) where TEntity : class
        {
            bool flag;
            if (removeEntityList == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                _dbContext.Set<TEntity>().RemoveRange(removeEntityList);
               _dbContext.SaveChanges();
                flag = true;
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }
        public virtual async Task<bool> RemoveMultipleEntityAsync<TEntity>(IEnumerable<TEntity> removeEntityList) where TEntity : class
        {
            var flag = false;
            if (removeEntityList == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                _dbContext.Set<TEntity>().RemoveRange(removeEntityList);
                await _dbContext.SaveChangesAsync();
                flag = true;
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }
        public virtual void Delete<TEntity>(object id) where TEntity : class
        {
            TEntity entityToDelete = _dbContext.Set<TEntity>().Find(id);
            Delete(entityToDelete);
           _dbContext.SaveChanges();
        }

        public virtual void Delete<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            var query = _dbContext.Set<TEntity>().Where(filter);
            _dbContext.Set<TEntity>().RemoveRange(query);

        }

        public virtual void Delete<TEntity>(TEntity entityToDelete) where TEntity : class
        {
            if (entityToDelete == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                _dbContext.Set<TEntity>().Remove(entityToDelete);
                _dbContext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public virtual void Update<TEntity>(TEntity entityToUpdate) where TEntity : class
        {
            if (entityToUpdate == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
                 _dbContext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }
        public Task UpdateMultipleEntity<TEntity>(IEnumerable<TEntity> entityList) where TEntity : class
        {
            if (entityList == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                
                _dbContext.Entry(entityList).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            throw new NotImplementedException();
        }
        public virtual DbParameter GetParameter()
        {
            return new SqlParameter();
        }
        public Task<DbParameter> GetParameterAsync()
        {
            throw new NotImplementedException();
        }
        public virtual DbParameter GetParameter(string paramName, System.Data.DbType dbtype, object value = null, bool bOutput = false)
        {
            var param = new SqlParameter();
            param.ParameterName = paramName;
            if (value != null)
            { param.Value = value; }
            param.DbType = dbtype;
            if (bOutput)
            { param.Direction = System.Data.ParameterDirection.Output; }
            return param;
        }
        public Task<DbParameter> GetParameterAsync(string paramName, DbType dbtype, object value = null, bool bOutput = false)
        {
            throw new NotImplementedException();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    _dbContext.Dispose();
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual IQueryable<TEntity> GetIQueryable<TEntity>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "") where TEntity : class
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>().AsNoTracking();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }
        public Task<IQueryable<TEntity>> GetIQueryableAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "") where TEntity : class
        {
            throw new NotImplementedException();
        }
               

        public Task DeleteAsync<TEntity>(object id) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync<TEntity>(TEntity entityToDelete) where TEntity : class
        {
            throw new NotImplementedException();
        }       

        public IDatabaseTransaction BeginTransaction()
        {
            return new EntityDatabaseTransaction(_dbContext);
        }

       
    }
}
