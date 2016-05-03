using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odonit.DAL.Models
{
    public class Exam
    {
        public Exam() {
            MedicalHistories = new HashSet<MedicalHistory>();
        }
        public int ExamId { get; set; }
        public string Extraoral { get; set; }
        public string Intraoral { get; set; }
        public string Others { get; set; }
        public string Observations { get; set; }
        [JsonIgnore]
        public virtual ICollection<MedicalHistory> MedicalHistories { get; set; }
    }
}
