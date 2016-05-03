using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using Odonit.DAL.Models;
using Odonit.DAL.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Odonit.DAL
{
    public class OdonitContext : DbContext
    {
        public OdonitContext()
            : base("Odonit") 
        {
            Database.SetInitializer<OdonitContext>(null);
            Database.Initialize(true);
        }

        public DbSet<Address> Address { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<MedicalHistory> MedicalHistory { get; set; }
        public DbSet<Exam> Exam { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<Tooth> Tooth { get; set; }
        public DbSet<Face> Face { get; set; }
        public DbSet<ToothService> ToothService { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Treatment> Treatment { get; set; }
        public DbSet<MedicalSecurity> MedicalSecurity { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new AddressConfig());
            modelBuilder.Configurations.Add(new ContactConfig());
            modelBuilder.Configurations.Add(new PersonConfig());
            modelBuilder.Configurations.Add(new MedicalHistoryConfig());
            modelBuilder.Configurations.Add(new ExamConfig());
            modelBuilder.Configurations.Add(new ServiceConfig());
            modelBuilder.Configurations.Add(new FaceConfig());
            modelBuilder.Configurations.Add(new ToothConfig());
            modelBuilder.Configurations.Add(new ToothServiceConfig());
            modelBuilder.Configurations.Add(new StatusConfig());
            modelBuilder.Configurations.Add(new TreatmentConfig());
            modelBuilder.Configurations.Add(new MedicalSecurityConfig());
        }
    }
}
