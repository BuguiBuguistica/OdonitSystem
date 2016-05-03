using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Odonit.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Odonit.DAL
{
    public class OdonitContext : DbContext
    {
        public OdonitContext()
            : base("OdonitContext")
        {

        }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<ObraSocial> ObraSociales { get; set; }
        public DbSet<Contacto> Contactos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}