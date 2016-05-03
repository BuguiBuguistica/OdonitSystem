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
    [RoutePrefix("api/Address")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    /// <summary>
    /// Address controller
    /// </summary>
    public class AddressController : ApiController
    {
        private readonly ILog _logger;
        private IService<Address> _service;
        public AddressController(ILog logger, IServiceFactory factory)
        {
            _logger = logger;
            _service = factory.CreateAddressService();
        }

        /// <summary>
        /// Add or Update address
        /// </summary>
        /// <param name="address">Address</param>
        /// <returns>Address</returns>
        [Route("v1")]
        [HttpPost]
        public Address AddOrUpdateAddress(Address address)
        {
            return _service.AddOrUpdate(address);
        }
    }
}