using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Odonit.DAL.Models;
using Odonit.DAL;
using log4net;

namespace Odonit.Business
{
    public class TreatmentService : IService<Treatment>
    {
        private readonly ILog _logger;
        private readonly IRepository<Treatment> _repository;

        public TreatmentService(ILog logger, IRepository<Treatment> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IEnumerable<Treatment> Get(System.Linq.Expressions.Expression<Func<Treatment, bool>> filter = null, Func<IQueryable<Treatment>, IOrderedQueryable<Treatment>> orderBy = null, string includeProperties = "")
        {
            IEnumerable<Treatment> treatments = null;
            try
            {
                treatments = _repository.Get(filter, null, includeProperties);

            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("There was an error getting treatments", ex);
            }
            return treatments;
        }

        public Treatment GetById(object id)
        {
            _logger.DebugFormat("Getting tratment Id: {0}", id);
            Treatment treatment = null;
            try
            {
                treatment = _repository.GetById(id);
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("There was an error getting tratment with Id: {0}.\r\nError: {1}.", id, ex);
            }

            return treatment;
        }

        public void Delete(object id)
        {
            _logger.Debug("Deleting treatment");
            try
            {
                _repository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.Error("There was error deleting treatment", ex);
            }
        }

        public Treatment AddOrUpdate(Treatment entity)
        {
            _logger.Debug("Adding or updating entity");
            try
            {
                return _repository.AddOrUpdate(entity);
            }
            catch (Exception ex)
            {
                _logger.Error("There was an error saving person", ex);
            }

            return null;
        }
    }
}
