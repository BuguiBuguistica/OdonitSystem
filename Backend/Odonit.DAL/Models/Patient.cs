using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odonit.DAL.Models
{
    public class Patient : Person
    {
        public int PatientId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public string MembershipId { get; set; }
    }
}
