using Odonit.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odonit.DAL
{
    public class Report
    {
         internal OdonitContext _context;

        public Report(OdonitContext context)
        {
            _context = context;  
        }
        public List<PatientsReport> get(ReportFilter filter)
        {

            List<PatientsReport> items = _context.Database.SqlQuery<PatientsReport>("getPatientsReport @ServiceId = {0}, @TreatmentId = {1}, @MedicalSecurityId= {2}, @FromDate= {3} , @EndDate= {4}", filter.ServiceId, filter.TreatmentId, filter.MedicalSecurityId, filter.FromDate, filter.EndDate).ToList();

            return items;
        }        

    }
}
