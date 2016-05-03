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
    public class ExamService : IService<Exam>
    {
        private readonly ILog _logger;
        private readonly IRepository<Exam> _repository;

        public ExamService(ILog logger, IRepository<Exam> repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public IEnumerable<Exam> Get(System.Linq.Expressions.Expression<Func<Exam, bool>> filter = null, Func<IQueryable<Exam>, IOrderedQueryable<Exam>> orderBy = null, string includeProperties = "")
        {
            IEnumerable<Exam> exam = null;
            return exam;
        }
        public Exam GetById(object it)
        {
            Exam exam = null;
            return exam;
        }
        public void Delete(object id) { }
        public Exam AddOrUpdate(Exam entity)
        {
            _logger.Debug("Adding or updating entity");
            try
            {
                return _repository.AddOrUpdate(entity);
            }
            catch (Exception ex)
            {
                _logger.Error("There was an error saving exam", ex);
            }

            return null;
        }
    }
}
