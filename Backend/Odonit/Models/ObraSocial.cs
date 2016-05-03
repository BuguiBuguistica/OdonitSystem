using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Odonit.Models
{
    public class ObraSocial
    {
        [Key]
        public int IdObraSocial { get; set; }
        public string Denominacion { get; set; }
        public string Abreviatura { get; set; }
    }
}