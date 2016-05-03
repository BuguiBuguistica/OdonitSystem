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
    public class ToothServiceService : IService<ToothService>
    {
        private readonly ILog _logger;
        private readonly IRepository<ToothService> _repository;

        public ToothServiceService(ILog logger, IRepository<ToothService> repository)
        {
            _logger = logger;
            _repository = repository;

        }

        public IEnumerable<ToothService> Get(System.Linq.Expressions.Expression<Func<ToothService, bool>> filter = null, Func<IQueryable<ToothService>, IOrderedQueryable<ToothService>> orderBy = null, string includeProperties = "")
        {
            IEnumerable<ToothService> toothServicies = null;
            try
            {
                toothServicies = _repository.Get(filter, null, includeProperties);

            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("There was an error getting toothService", ex);
            }
            return toothServicies;
        }

        public ToothService GetById(object id)
        {
            _logger.DebugFormat("Getting toothService Id: {0}", id);
            ToothService toothService = null;
            try
            {
                toothService = _repository.GetById(id);
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("There was an error getting tooth service with Id: {0}.\r\nError: {1}.", id, ex);
            }

            return toothService;
        }

        public void Delete(object id)
        {
            _logger.Debug("Deleting tooth service");
            try
            {
                _repository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.Error("There was error deleting tooth service", ex);
            }
        }

        public ToothService AddOrUpdate(ToothService entity)
        {
            _logger.Debug("Adding or updating entity");
            try
            {
                return _repository.AddOrUpdate(entity);
            }
            catch (Exception ex)
            {
                throw ex;
                _logger.Error("There was an error saving tooth service", ex);
            }

            return null;
        }
    }
}
