using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odonit.DAL.Models
{
    public class ToothService
    {       
        public int ToothServiceId { get; set; }
        public string Diagnosis { get; set; }        
        public DateTime? InitialDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Amount { get; set; }//Valor
        public decimal Payment { get; set; }//Abonado
        public decimal Remainder { get; set; }//Saldo
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        public int? ToothId { get; set; }
        public virtual Tooth Tooth { get; private set; }
        public int? FaceId { get; set; }
        public virtual Face Face{get; private set;}
        public int? ServiceId { get; set; }
        public virtual Service Service { get; private set; }
        public int? StatusId { get; set; }
        public virtual Status Status { get; private set; }
        public int? TreatmentId { get; set; } //Tratamiento relacionado con las obras sociales
        public virtual Treatment Treatment { get; private set; }
    }
}
