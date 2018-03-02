using System;
using System.Linq;
using System.Threading.Tasks;
using DataInitializer;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;

namespace Repository.Implementation
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : ApplicationDbContext
    {
        private ApplicationDbContext DbContext { get; }

        private bool _disposed;

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                }
            }

            _disposed = true;
        }

        public UnitOfWork(TContext context)
        {
            DbContext = context ?? throw new ArgumentException(nameof(context));

            _disposed = false;
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return DbContext.Database.ExecuteSqlCommand(sql, parameters);
        }

        public IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class
        {
            return DbContext.Set<TEntity>().FromSql(sql, parameters);
        }

        public int SaveChanges(bool ensureAutoHistory = false)
        {
            return DbContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync(bool ensureAutoHistory = false)
        {
            return DbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
