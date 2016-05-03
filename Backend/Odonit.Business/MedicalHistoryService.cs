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
    public class MedicalHistoryService : IService<MedicalHistory>
    {
        private readonly ILog _logger;
        private readonly IRepository<MedicalHistory> _repository;

        public MedicalHistoryService(ILog logger, IRepository<MedicalHistory> repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public IEnumerable<MedicalHistory> Get(System.Linq.Expressions.Expression<Func<MedicalHistory, bool>> filter = null, Func<IQueryable<MedicalHistory>, IOrderedQueryable<MedicalHistory>> orderBy = null, string includeProperties = "")
        {
            IEnumerable<MedicalHistory> medicalHistories = null;
            try
            {
                medicalHistories = _repository.Get(filter, null, includeProperties);

            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("There was an error getting medical history", ex);
            }
            return medicalHistories;
        }

        public MedicalHistory GetById(object id)
        {
            _logger.DebugFormat("Getting medicalHistory Id: {0}", id);
            MedicalHistory medicalHistory = null;
            try
            {
                medicalHistory = _repository.GetById(id);
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("There was an error getting medical history with Id: {0}.\r\nError: {1}.", id, ex);
            }

            return medicalHistory;
        }

        public void Delete(object id)
        {
            _logger.Debug("Deleting medical history");
            try
            {
                _repository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.Error("There was error deleting medical history", ex);
            }
        }

        public MedicalHistory AddOrUpdate(MedicalHistory entity)
        {
            _logger.Debug("Adding or updating entity");
            try
            {
                return _repository.AddOrUpdate(entity);
            }
            catch (Exception ex)
            {
                _logger.Error("There was an error saving medical history", ex);
            }

            return null;
        }
    }
}
