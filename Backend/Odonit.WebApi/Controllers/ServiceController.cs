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
    [RoutePrefix("api/Services")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    /// <summary>
    /// Service controller
    /// </summary>
    public class ServiceController : ApiController
    {
        private readonly ILog _logger;
        private IService<Service> _service;

        public ServiceController(ILog logger, IServiceFactory factory)
        {
            _logger = logger;
            _service = factory.CreateServicesService();
        }

        /// <summary>
        /// Get Services
        /// </summary>
        /// <returns>A list of Service</returns>
        [Route("v1")]
        [HttpGet]
        public IEnumerable<Service> GetTreatment()
        {
            IEnumerable<Service> services;
            services = _service.Get();
            return services;
        }
	}
}