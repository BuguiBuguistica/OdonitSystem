using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odonit.DAL.Models
{
    public class MedicalHistory
    {
        public MedicalHistory()
        {            
            People = new HashSet<Person>();
        }
        public int MedicalHistoryId { get; set; }
        public string CurrentMedication { get; set; }
        public string BloodGroup { get; set; }
        public bool? Alergic { get; set; }
        public bool? Hemorrhage { get; set; }
        public bool? Diabetes { get; set; }
        public bool? HeartDisease { get; set; }
        public bool? RespiratoryDisease { get; set; }
        public bool? KidneyDisease { get; set; }
        public bool? Hepatitis { get; set; }
        public bool? Hypertension { get; set; }
        public bool? STD { get; set; }
        public DateTime? PregnancyDate { get; set; }
        public DateTime? TransfusionDate { get; set; }
        public string Observations { get; set; }
        public string Habits { get; set; }
        public string Others { get; set; }
        public int? ExamId { get; set; }
        public virtual Exam Exam { get; set; }
        [JsonIgnore]
        public virtual ICollection<Person> People { get; set; }
    }
}
