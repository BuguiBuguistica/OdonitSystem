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
    public class PersonService : IService<Person>
    {
        private readonly ILog _logger;
        private readonly IRepository<Person> _repository;
       
        public PersonService(ILog logger, IRepository<Person> repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public IEnumerable<Person> Get(System.Linq.Expressions.Expression<Func<Person, bool>> filter = null, Func<IQueryable<Person>, IOrderedQueryable<Person>> orderBy = null, string includeProperties = "")
        {            
            IEnumerable<Person> persons = null;
            try
            {
                persons = _repository.Get(filter,null,includeProperties);                

            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("There was an error getting persons", ex);
            }
            return persons;
        }
        
        public Person GetById(object id)
        {
            _logger.DebugFormat("Getting person Id: {0}", id);
            Person person = null;
            try
            {
                person = _repository.GetById(id);
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("There was an error getting person with Id: {0}.\r\nError: {1}.", id, ex);
            }

            return person;
        }

        public void Delete(object id)
        {
            _logger.Debug("Deleting person");
            try
            {
                _repository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.Error("There was error deleting person", ex);
            }
        }

        public Person AddOrUpdate(Person entity)
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
