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
    [RoutePrefix("api/Contact")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    /// <summary>
    /// Contact controller
    /// </summary>
    public class ContactController : ApiController
    {
        private readonly ILog _logger;
        private IService<Contact> _service;
       
        public ContactController(ILog logger, IServiceFactory factory)
        {
            _logger = logger;
            _service = factory.CreateContactService();
        }

        /// <summary>
        /// Add or Update contact
        /// </summary>
        /// <param name="contact">Contact</param>
        /// <returns>Contact</returns>
        [Route("v1")]
        [HttpPost]
        public Contact AddOrUpdateContact(Contact contact)
        {
            return _service.AddOrUpdate(contact);
        }
    }
}