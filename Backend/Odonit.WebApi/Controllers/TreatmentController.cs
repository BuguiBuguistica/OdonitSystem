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
    [RoutePrefix("api/Treament")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    /// <summary>
    /// Treatment controller
    /// </summary>
    public class TreatmentController : ApiController
    {
        private readonly ILog _logger;
        private IService<Treatment> _service;

        public TreatmentController(ILog logger, IServiceFactory factory)
        {
            _logger = logger;
            _service = factory.CreateTreatmentService();
        }

        /// <summary>
        /// Get a list of Treatment
        /// </summary>
        /// <returns>List of Treatment</returns>
        [Route("v1")]
        [HttpGet]
        public IEnumerable<Treatment> GetTreatment()
        {
            IEnumerable<Treatment> tratments;
            tratments = _service.Get();
            return tratments;
        }
    }
}