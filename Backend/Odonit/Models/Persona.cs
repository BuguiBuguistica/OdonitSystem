using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Odonit.Models
{
    public class Persona
    {
        [Key]
        public int IdPersona { get; set; } // IdPersona (Primary key)
        public bool? EsPaciente { get; set; } // EsPaciente
        public string Nombre { get; set; } // Nombre
        public string Apellido { get; set; } // Apellido
        public decimal? Edad { get; internal set; } // Edad
        public DateTime? FechaNacimiento { get; set; } // FechaNacimiento
        public bool? Sexo { get; set; } // Sexo
        public int NumeroIdentificacion { get; set; } // NumeroIdentificacion
        public int? IdDomicilio { get; set; } // IdDomicilio
        public int? IdContacto { get; set; } // IdContacto

        // Reverse navigation
        //public virtual Odontologo Odontologo { get; set; } // Odontologo.FK_Odontologo_Persona
        //[ForeignKey("IdPersona")]
        //public virtual Paciente Paciente { get; set; } // Paciente.FK_Paciente_Persona

        // Foreign keys
        [ForeignKey("IdContacto")]
        public virtual Contacto Contacto { get; set; } // FK_Persona_Contacto
        //public virtual Domicilio Domicilio { get; set; } // FK_Persona_Domicilio
    }
}