using log4net;
using Odonit.DAL;
using Odonit.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odonit.Business
{
    public class ServiceService : IService<Service>
    {
        private readonly ILog _logger;
        private readonly IRepository<Service> _repository;

        public ServiceService(ILog logger, IRepository<Service> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IEnumerable<Service> Get(System.Linq.Expressions.Expression<Func<Service, bool>> filter = null, Func<IQueryable<Service>, IOrderedQueryable<Service>> orderBy = null, string includeProperties = "")
        {
            IEnumerable<Service> services = null;
            try
            {
                services = _repository.Get(filter, null, includeProperties);

            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("There was an error getting services", ex);
            }
            return services;
        }
        public Service GetById(object it) { 
            Service service = null;
            return service;
        }
        public void Delete(object id) { }
        public Service AddOrUpdate(Service entity) {
            Service service = null;
            return service;
        }
    }
}
