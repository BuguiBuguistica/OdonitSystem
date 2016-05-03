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
    [RoutePrefix("api/MedicalHistory")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    /// <summary>
    /// Medical History controller
    /// </summary>
    public class MedicalHistoryController : ApiController
    {
        private readonly ILog _logger;
        private IService<MedicalHistory> _service;
        
        public MedicalHistoryController(ILog logger, IServiceFactory factory)
        {
            _logger = logger;
            _service = factory.CreateMedicalHistoryService();
        }

        /// <summary>
        /// Add or Update MedicalHistory
        /// </summary>
        /// <param name="medicalHistory">MedicalHistory</param>
        /// <returns>MedicalHistory</returns>
        [Route("v1")]
        [HttpPost]
        public MedicalHistory AddOrUpdateMedicalHistory(MedicalHistory medicalHistory)
        {
            return _service.AddOrUpdate(medicalHistory);
        }
    }
}