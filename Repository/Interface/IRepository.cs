using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Repository.Interface
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate = null, bool disableTracking = true);

        IQueryable<TEntity> FromSql(string sql, params object[] parameters);
    }
}
