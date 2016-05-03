using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Odonit.DAL;

namespace Odonit.Business
{
    public class EntityService<T> : IService<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public EntityService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public IEnumerable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, string includeProperties)
        {
            return _repository.Get(filter, orderBy, includeProperties);
        }

        public T GetById(object id)
        {
            return _repository.GetById(id);
        }

        public void Delete(object id)
        {
            _repository.Delete(id);
        }

        public T AddOrUpdate(T entity)
        {
            return _repository.AddOrUpdate(entity);
        }

    }
}
