using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odonit.DAL.Models
{
    public class PatientsReport
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PersonCode { get; set; }
        public string Address { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
    }
}
