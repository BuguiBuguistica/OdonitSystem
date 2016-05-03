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
    [RoutePrefix("api/ToothService")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    /// <summary>
    /// ToothService controller
    /// </summary>
    public class ToothServiceController : ApiController
    {
        private readonly ILog _logger;
        private IService<ToothService> _service;

        public ToothServiceController(ILog logger, IServiceFactory factory)
        {
            _logger = logger;
            _service = factory.CreateToothServiceService();
        }
        /// <summary>
        /// Add or Update ToothService
        /// </summary>
        /// <param name="toothService">ToothService</param>
        /// <returns>ToothService</returns>
        [Route("v1")]
        [HttpPost]
        public ToothService AddOrUpdatePerson(ToothService toothService)
        {
           return _service.AddOrUpdate(toothService);            
        }

        /// <summary>
        /// Get a list of ToothService
        /// </summary>
        /// <param name="serviceId">serviceId</param>
        /// <param name="personId">personId</param>
        /// <param name="fromDate">fromDate</param>
        /// <param name="toDate">toDate</param>
        /// <param name="statusId">statusId</param>
        /// <param name="treatmentId">treatmentId</param>
        /// <returns>List of ToothService</returns>
        [Route("v1")]
        [HttpGet]
        public IEnumerable<ToothService> GetToothServices(int? serviceId = null, int personId = 0, string fromDate = "", string toDate = "", int? statusId = null, int? treatmentId = null)
        {
            DateTime? from = !string.IsNullOrEmpty(fromDate) ? DateTime.Parse(fromDate) : (DateTime?)null;
            DateTime? to = !string.IsNullOrEmpty(toDate) ? DateTime.Parse(toDate) : (DateTime?)null;
            IEnumerable<ToothService> toothServices;

            System.Linq.Expressions.Expression<Func<ToothService, bool>> expr = ts => (
                ts.PersonId.Equals(personId) && (!serviceId.HasValue || ts.Service.ServiceId == serviceId) &&
                ((!from.HasValue || ts.InitialDate >= from) && (!to.HasValue || ts.InitialDate <= to)) &&
                (!statusId.HasValue || ts.StatusId == statusId) && (!treatmentId.HasValue || ts.TreatmentId == treatmentId)
                );
            toothServices = _service.Get(expr, null, "");
            return toothServices;
        }
	}
}