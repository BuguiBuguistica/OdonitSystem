using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Odonit.DAL
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        T GetById(object id);
        void Delete(object id);
        void SaveContext();
        T AddOrUpdate(T entity);
        void Dispose();
    }
}
