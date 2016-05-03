using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using log4net;
using Odonit.DAL.Models;
using Odonit.Business;
using Newtonsoft.Json;

namespace Odonit.WebApi.Controllers
{
    [RoutePrefix("api/Person")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    /// <summary>
    /// Person controller
    /// </summary>
    public class PersonController : ApiController
    {
        private readonly ILog _logger;
        private IService<Person> _service;
              
        public PersonController(ILog logger, IServiceFactory factory)
        {
            _logger = logger;
            _service = factory.CreatePersonService();
        }

        /// <summary>
        /// Add or Update Person
        /// </summary>
        /// <param name="person">Person</param>
        /// <returns>Person</returns>
        [Route("v1")]
        [HttpPost]
        public Person AddOrUpdatePerson(Person person)
        {
            return _service.AddOrUpdate(person);
        }

        /// <summary>
        /// Get Person by Id
        /// </summary>
        /// <param name="Id">PersonId</param>
        /// <returns>Person</returns>
        [Route("v1")]
        [HttpGet]
        public Person GetPersonById(int Id)
        {
            return _service.GetById(Id);
        }

        /// <summary>
        /// Get persons filtering by params
        /// </summary>
        /// <param name="lastName">lastName</param>
        /// <param name="code">code</param>
        /// <param name="medicalSecurityId">medicalSecurityId</param>
        /// <param name="isNoActive">isNoActive</param>
        /// <param name="medicalHistoryId">medicalHistoryId</param>
        /// <returns></returns>
        [Route("v1")]
        [HttpGet]
        public IEnumerable<Person> GetPersons(string lastName = "", string code = "", int? medicalSecurityId = null, bool? isNoActive = false, int? medicalHistoryId = null)
        {
            IEnumerable<Person> persons;
           
            System.Linq.Expressions.Expression<Func<Person, bool>> expr = person => (
                (string.IsNullOrEmpty(lastName)|| person.LastName.StartsWith(lastName)) && 
                (string.IsNullOrEmpty(code) || person.Code.ToString().StartsWith(code)) &&
                (!medicalSecurityId.HasValue || person.MedicalSecurityId == medicalSecurityId) &&
                (!medicalHistoryId.HasValue || person.MedicalHistoryId == medicalHistoryId) &&
                (person.IsActive != isNoActive && person.IsActive != null)
                );
            persons = _service.Get(expr, null, "Contact");
            return persons;
        }
    }
}
