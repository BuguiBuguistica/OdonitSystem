using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Odonit.DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private const string Comma = ",";
        internal OdonitContext _context;
        internal DbSet<T> _dbSet;

        public Repository(OdonitContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
            
            //_context.Configuration.ProxyCreationEnabled = false;
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = " ")
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new string[] { Comma }, StringSplitOptions.RemoveEmptyEntries))
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

        public T GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public void Delete(object id)
        {
            T entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public void SaveContext()
        {
            _context.SaveChanges();
        }

        public T AddOrUpdate(T entity)
        {
            _dbSet.AddOrUpdate<T>(entity);
            _context.SaveChanges();
            return entity;
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
