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
    [RoutePrefix("api/Exam")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    /// <summary>
    /// Exam controller
    /// </summary>
    public class ExamController : ApiController
    {
        private readonly ILog _logger;
        private IService<Exam> _service;
        public ExamController(ILog logger, IServiceFactory factory)
        {
            _logger = logger;
            _service = factory.CreateExamService();
        }

        /// <summary>
        /// Add or Update exam
        /// </summary>
        /// <param name="address">Exam</param>
        /// <returns>Exam</returns>
        [Route("v1")]
        [HttpPost]
        public Exam AddOrUpdateExam(Exam exam)
        {
            return _service.AddOrUpdate(exam);
        }
    }
}