using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate = null, bool disableTracking = true);

        IQueryable<TEntity> FromSql(string sql, params object[] parameters);

        TEntity Find(params object[] keyValues);

        Task<TEntity> FindAsync(params object[] keyValues);

        Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken);

        void Insert(TEntity entity);

        void Insert(params TEntity[] entities);

        void Insert(IEnumerable<TEntity> entities);

        void BulkInsert(IList<TEntity> entities);

        Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        Task InsertAsync(params TEntity[] entities);

        Task BulkInsertAsync(IList<TEntity> entities);

        Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

        void Update(TEntity entity);

        void Update(params TEntity[] entities);

        void Update(IEnumerable<TEntity> entities);

        void Delete(object id);

        void Delete(TEntity entity);

        void Delete(params TEntity[] entities);

        void Delete(IEnumerable<TEntity> entities);

        IQueryable<TEntity> GetAll();

        TEntity Get(Guid id);

        TEntity Get(String id);
    }
}
