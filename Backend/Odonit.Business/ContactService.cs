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
    public class ContactService : IService<Contact>
    {
        private readonly ILog _logger;
        private readonly IRepository<Contact> _repository;

        public ContactService(ILog logger, IRepository<Contact> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IEnumerable<Contact> Get(System.Linq.Expressions.Expression<Func<Contact, bool>> filter = null, Func<IQueryable<Contact>, IOrderedQueryable<Contact>> orderBy = null, string includeProperties = "")
        {
            IEnumerable<Contact> contact = null;

            return contact;
        }
        public Contact GetById(object it)
        {
            Contact contact = null;
            return contact;
        }
        public void Delete(object id) { }
        public Contact AddOrUpdate(Contact entity)
        {
            _logger.Debug("Adding or updating entity");
            try
            {
                return _repository.AddOrUpdate(entity);
            }
            catch (Exception ex)
            {
                _logger.Error("There was an error saving contact", ex);
            }

            return null;
        }
    }
}
