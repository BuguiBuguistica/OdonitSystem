using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Odonit.Models
{
    public partial class Paciente
    {
        [Key]
        public int IdPaciente { get; set; } // IdPaciente (Primary key)
        public DateTime? FechaIngreso { get; set; } // FechaIngreso
        public bool Activo { get; set; } // Activo
        public string NumeroAfiliado { get; set; } // NumeroAfiliado
        public int? IdObraSocial { get; set; } // IdObraSocial
        public int? IdHistoriaClinica { get; set; } // IdHistoriaClinica
        // Reverse navigation
        //public virtual ICollection<Prestacion> Prestacions { get; set; } // Prestacion.FK_Prestacion_Paciente

        // Foreign keys
        //public virtual HistoriaClinica HistoriaClinica { get; set; } // FK_Paciente_HistoriaClinica
        //[ForeignKey("IdPaciente")]
        //public virtual Persona Persona { get; set; } // FK_Paciente_Persona
        public Paciente()
        {
            FechaIngreso = System.DateTime.Now;
            Activo = true;
            //Prestacions = new List<Prestacion>();
            InitializePartial();
        }
        partial void InitializePartial();
    }
}