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
    public class MedicalSecurityService : IService<MedicalSecurity>
    {
        private readonly ILog _logger;
        private readonly IRepository<MedicalSecurity> _repository;

        public MedicalSecurityService(ILog logger, IRepository<MedicalSecurity> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IEnumerable<MedicalSecurity> Get(System.Linq.Expressions.Expression<Func<MedicalSecurity, bool>> filter = null, Func<IQueryable<MedicalSecurity>, IOrderedQueryable<MedicalSecurity>> orderBy = null, string includeProperties = "")
        {
            IEnumerable<MedicalSecurity> medicalSecurity = null;
            try
            {
                medicalSecurity = _repository.Get(filter, null, includeProperties);

            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("There was an error getting services", ex);
            }
            return medicalSecurity;
        }
        public MedicalSecurity GetById(object it) {
            MedicalSecurity medicalSecurity = null;
            return medicalSecurity;
        }
        public void Delete(object id) { }
        public MedicalSecurity AddOrUpdate(MedicalSecurity entity)
        {
            MedicalSecurity medicalSecurity = null;
            return medicalSecurity;
        }
    }
}
