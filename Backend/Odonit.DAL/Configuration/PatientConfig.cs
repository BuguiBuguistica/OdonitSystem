using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Odonit.DAL.Models;

namespace Odonit.DAL.Configuration
{
    public class PatientConfig : EntityTypeConfiguration<Patient>
    {
        public PatientConfig()
        {
            ToTable("Patient");
            Property(p => p.MembershipId);
            Property(p => p.CreatedDate);
            Property(p => p.IsActive);
        }
    }
}
