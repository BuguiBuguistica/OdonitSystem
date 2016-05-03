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
    public class AddressService : IService<Address>
    {
        private readonly ILog _logger;
        private readonly IRepository<Address> _repository;

        public AddressService(ILog logger, IRepository<Address> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IEnumerable<Address> Get(System.Linq.Expressions.Expression<Func<Address, bool>> filter = null, Func<IQueryable<Address>, IOrderedQueryable<Address>> orderBy = null, string includeProperties = "")
        {
            IEnumerable<Address> address = null;

            return address;
        }
        public Address GetById(object it)
        {
            Address address = null;
            return address;
        }
        public void Delete(object id) { }
        public Address AddOrUpdate(Address entity)
        {
            _logger.Debug("Adding or updating entity");
            try
            {
                return _repository.AddOrUpdate(entity);
            }
            catch (Exception ex)
            {                
                _logger.Error("There was an error saving address", ex);                            
            }

            return null;
        }
    }
}
