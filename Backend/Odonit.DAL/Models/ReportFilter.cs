using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odonit.DAL.Models
{
    public class ReportFilter
    {
        public string ServiceName { get; set; }
        public int? ServiceId {get;set;}
        public string TreatmentName { get; set; }
        public int? TreatmentId {get;set;}
        public string MedicalSecurityName { get; set; }
        public int? MedicalSecurityId {get;set;}
        public DateTime? FromDate {get;set;}
        public DateTime? EndDate { get; set; }
    }
}
