using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Odonit.Models
{
    public partial class Contacto
    {
        [Key]
        public int IdContacto { get; set; } // IdContacto (Primary key)
        public string TelFijo { get; set; } // TelFijo
        public string Celular { get; set; } // Celular
        public string Email { get; set; } // Email

        // Reverse navigation
        //public virtual ICollection<Persona> Personas { get; set; } // Persona.FK_Persona_Contacto

        //public Contacto()
        //{
        //    Personas = new List<Persona>();
        //    InitializePartial();
        //}
        //partial void InitializePartial();
    }
}