using log4net;
using Odonit.Business;
using Odonit.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Odonit.WebApi.Controllers
{
    [RoutePrefix("api/MedicalSecurity")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    /// <summary>
    /// Medical Security controller
    /// </summary>
    public class MedicalSecurityController : ApiController
    {
        private readonly ILog _logger;
        private IService<MedicalSecurity> _service;
       
        public MedicalSecurityController(ILog logger, IServiceFactory factory)
        {
            _logger = logger;
            _service = factory.CreateMedicalSecurityService();
        }
        /// <summary>
        /// Get a list of Medical Security
        /// </summary>
        /// <returns>List of MedicalSecurity</returns>
        [Route("v1")]
        [HttpGet]
        public IEnumerable<MedicalSecurity> GetMedicalSecurity()
        {
            IEnumerable<MedicalSecurity> services;
            services = _service.Get();
            return services;
        }
    }
}