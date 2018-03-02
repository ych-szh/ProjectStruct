using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using DataInitializer;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;

namespace Repository.Implementation
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;

        protected ApplicationDbContext DbContext { get; }

        public Repository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = DbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate = null, bool disableTracking = true)
        {
            var set = disableTracking ? _dbSet.AsNoTracking() : _dbSet;

            if (predicate != null)
            {
                set = set.Where(predicate);
            }

            return set;
        }

        public IQueryable<TEntity> FromSql(string sql, params object[] parameters)
        {
            return _dbSet.FromSql(sql, parameters);
        }

        public TEntity Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public Task<TEntity> FindAsync(params object[] keyValues)
        {
            return _dbSet.FindAsync(keyValues);
        }

        public Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken)
        {
            return _dbSet.FindAsync(keyValues, cancellationToken);
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Insert(params TEntity[] entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Insert(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void BulkInsert(IList<TEntity> entities)
        {
            DbContext.BulkInsert(entities);
        }

        public Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _dbSet.AddAsync(entity, cancellationToken);
        }

        public Task InsertAsync(params TEntity[] entities)
        {
            return _dbSet.AddRangeAsync(entities);
        }

        public Task BulkInsertAsync(IList<TEntity> entities)
        {
            return DbContext.BulkInsertAsync(entities);
        }

        public Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _dbSet.AddRangeAsync(entities, cancellationToken);
        }

        public void Update(TEntity entity)
        {
            _dbSet.UpdateRange(entity);
        }

        public void Update(params TEntity[] entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public void Delete(object id)
        {
            var typeInfo = typeof(TEntity).GetTypeInfo();

            var key = DbContext.Model.FindEntityType(typeInfo.Name).FindPrimaryKey().Properties.FirstOrDefault();
            if (key == null)
            {
                return;
            }

            var property = typeInfo.GetProperty(key.Name);
            if (property != null)
            {
                var entity = Activator.CreateInstance<TEntity>();
                property.SetValue(entity, id);
                DbContext.Entry(entity).State = EntityState.Deleted;
            }
            else
            {
                var entity = _dbSet.Find(id);
                if (entity != null)
                {
                    Delete(entity);
                }
            }
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void Delete(params TEntity[] entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public TEntity Get(Guid id)
        {
            return _dbSet.Find(id);
        }

        public TEntity Get(string id)
        {
            return _dbSet.Find(id);
        }
    }
}
