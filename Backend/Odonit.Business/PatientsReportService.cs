using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Odonit.DAL;
using Odonit.DAL.Models;
using log4net;

namespace Odonit.Business
{
    public class PatientsReportService
    {
        private readonly ILog _logger;
        private readonly Report _report;

        public PatientsReportService(ILog logger, Report report)
        {
            _logger = logger;
            _report = report;
        }
        public List<PatientsReport> get(ReportFilter filter)
        {
            var items = _report.get(filter);
            return items;
        }
    }
}
