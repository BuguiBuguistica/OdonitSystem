using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Odonit.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Odonit.DAL.Configuration
{
    public class PersonConfig : EntityTypeConfiguration<Person>
    {
        public PersonConfig()
        {
            ToTable("Person");
            HasKey(x => x.PersonId);
            Property(x => x.FirstName);
            Property(x => x.LastName);
            Property(x => x.Gender);
            Property(x => x.BirthDate);
            Property(x => x.Code);
            //Property(x => x.MedicalSecurity);
            Property(x => x.SecurityPlan);
            Property(x => x.SecurityNumber);
            Property(x => x.IsActive);
            Property(x => x.Age);
            HasOptional(x => x.MedicalSecurity);
            HasOptional(x => x.Contact).WithMany(c => c.People).HasForeignKey(x => x.ContactId).WillCascadeOnDelete(true);
            HasOptional(x => x.Address).WithMany(a => a.People).HasForeignKey(x => x.AddressId).WillCascadeOnDelete(true);
            HasOptional(x => x.MedicalHistory).WithMany(a => a.People).HasForeignKey(x => x.MedicalHistoryId);
        }
    }
}
