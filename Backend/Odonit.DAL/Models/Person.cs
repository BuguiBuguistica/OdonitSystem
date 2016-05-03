using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Odonit.DAL.Models
{
    public class Person
    {
        public int PersonId { get; set; } // IdPersona (Primary key)
        public string FirstName { get; set; } // Nombre
        public string LastName { get; set; } // Apellido
        public int? Age { get; set; } // Edad
        public DateTime? BirthDate { get; set; } // FechaNacimiento
        public bool? Gender { get; set; } // Sexo
        public int Code { get; set; } // NumeroIdentificacion
        public int? AddressId { get; set; } // IdDomicilio
        public int? ContactId { get; set; } // IdContacto
        public virtual Address Address { get; set; }
        public virtual Contact Contact { get; set; }
        public int? MedicalHistoryId { get; set; } // IdMedicalHistory
        public virtual MedicalHistory MedicalHistory { get; set; } //ObraSocial
        //public string MedicalSecurity { get; set; } //ObraSocial
        public virtual MedicalSecurity MedicalSecurity { get; set; }
        public int? MedicalSecurityId { get; set; }
        public string SecurityPlan { get; set; } //Plan
        public int? SecurityNumber { get; set; } //NumeroAfiliado
        public bool? IsActive { get; set; } //PacienteActivo        
    }
}
